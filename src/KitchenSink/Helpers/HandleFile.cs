using System;
using System.IO;
using System.Web;
using System.Collections.Concurrent;
using System.Collections.Specialized;
using Starcounter;

namespace KitchenSink.Helpers
{
    public static class HandleFile
    {
        public const string WebSocketGroupName = "StarcounterFileUploadWebSocketGroup";

        public static readonly ConcurrentDictionary<ulong, UploadTask> Uploads =
            new ConcurrentDictionary<ulong, UploadTask>();

        public static void GET(string Url, Action<UploadTask> UploadingAction)
        {
            string url = Url + "?{?}";

            Handle.GET(url, (string parameters, Request request) =>
            {
                if (!ResolveUploadParameters(
                    parameters,
                    out string sessionId,
                    out string fileName,
                    out long fileSize,
                    out string error))
                {
                    return new Response()
                    {
                        StatusCode = (ushort)System.Net.HttpStatusCode.BadRequest,
                        Body = error
                    };
                }

                if (!request.WebSocketUpgrade)
                {
                    return 404;
                }

                WebSocket ws = request.SendUpgrade(WebSocketGroupName);
                var task = new UploadTask(sessionId, fileName, fileSize, parameters);

                task.StateChange += (s, a) => UploadingAction(s as UploadTask);

                if (!Uploads.TryAdd(ws.ToUInt64(), task))
                {
                    return new Response()
                    {
                        StatusCode = (ushort)System.Net.HttpStatusCode.BadRequest,
                        Body = "Unable to create upload task"
                    };
                }

                return HandlerStatus.Handled;
            }, new HandlerOptions() { SkipRequestFilters = true });

            Handle.WebSocket(WebSocketGroupName, (byte[] data, WebSocket ws) =>
            {
                if (!Uploads.ContainsKey(ws.ToUInt64()))
                {
                    ws.Disconnect("Could not find correct socket to handle the incoming data.",
                        WebSocket.WebSocketCloseCodes.WS_CLOSE_CANT_ACCEPT_DATA);
                    return;
                }

                UploadTask task = Uploads[ws.ToUInt64()];

                task.Write(data);

                if (task.FileSize > 0)
                {
                    ws.Send(task.Progress.ToString());
                }
            });

            Handle.WebSocketDisconnect(WebSocketGroupName, (ws) =>
            {

                if (Uploads.TryRemove(ws.ToUInt64(), out UploadTask task))
                {
                    task.Close();
                }
            });
        }

        private static bool ResolveUploadParameters(
            string Parameters,
            out string SessionId,
            out string FileName,
            out long FileSize,
            out string Error)
        {
            FileName = null;
            FileSize = -1;
            Error = null;

            NameValueCollection values = HttpUtility.ParseQueryString(Parameters);

            SessionId = values["sessionid"];

            if (string.IsNullOrEmpty(SessionId))
            {
                Error = "Invalid or missing sessionid url parameter";
                return false;
            }

            FileName = values["filename"];

            if (string.IsNullOrEmpty(FileName))
            {
                Error = "Invalid or missing filename url parameter";
                return false;
            }

            if (!long.TryParse(values["filesize"], out FileSize))
            {
                Error = "Invalid or missing filesize url parameter";
                return false;
            }

            return true;
        }

        public enum UploadTaskState
        {
            Connected,
            Uploading,
            Completed,
            Error
        }

        public class UploadTask
        {
            public event EventHandler StateChange;

            /// <summary>
            /// Size of uploaded file
            /// </summary>
            public long FileSize { get; protected set; }

            /// <summary>
            /// Name of uploaded file
            /// </summary>
            public string FileName { get; protected set; }

            /// <summary>
            /// Temporary path of uploaded file
            /// </summary>
            public string FilePath { get; protected set; }

            /// <summary>
            /// Starcounter session id
            /// </summary>
            public string SessionId { get; protected set; }

            /// <summary>
            /// Query string parameters
            /// </summary>
            public string QueryString { get; protected set; }

            /// <summary>
            /// Indicates current state of the task
            /// </summary>
            public UploadTaskState State { get; protected set; }

            protected FileStream FileStream;

            public UploadTask(string SessionId, string FileName, long FileSize, string QueryString)
            {
                this.SessionId = SessionId;
                this.FileName = FileName;
                this.FileSize = FileSize;
                this.QueryString = QueryString;

                this.State = UploadTaskState.Connected;
                this.FilePath = Path.GetTempFileName();
                this.FileStream = new FileStream(this.FilePath, FileMode.Append);
            }

            public string TempFileName => this.FileStream?.Name;

            public int Progress
            {
                get
                {
                    if (this.FileSize < 1 || this.FileStream == null)
                    {
                        return 0;
                    }

                    if (this.State == UploadTaskState.Completed)
                    {
                        return 100;
                    }

                    if (this.State == UploadTaskState.Error)
                    {
                        return -1;
                    }

                    int progress = (int)(100.0 * this.FileStream.Position / this.FileSize);

                    return progress;
                }
            }

            public void Write(byte[] Data)
            {
                this.State = UploadTaskState.Uploading;
                this.FileStream.Write(Data, 0, Data.Length);
                this.FileStream.Flush(true);
                this.OnUploading();
            }

            public void Close()
            {
                if (this.Progress >= 100)
                {
                    this.State = UploadTaskState.Completed;
                }
                else
                {
                    this.State = UploadTaskState.Error;
                }

                if (this.FileStream != null)
                {
                    this.FileStream.Dispose();
                }

                this.OnUploading();
            }

            protected void OnUploading()
            {
                this.StateChange?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}