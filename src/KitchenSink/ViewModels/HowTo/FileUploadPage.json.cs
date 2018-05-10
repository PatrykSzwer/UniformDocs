using System;
using System.IO;
using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class FileUploadPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.SessionId = Session.Current.SessionId;
        }

        public string GetFileSizeString(long inputSize)
        {
            string[] sizes = { "Bytes", "KB", "MB", "GB", "TB" };

            if (inputSize == 0)
            {
                return "0 Byte";
            }

            int i = (int)(Math.Floor(Math.Log(inputSize) / Math.Log(1024)));
            string size = Math.Round(inputSize / Math.Pow(1024, i), 2) + " " + sizes[i];

            return size;
        }

        [FileUploadPage_json.Files]
        partial class FileUploadFilePage : Json
        {
            static FileUploadFilePage()
            {
                DefaultTemplate.FileSizeString.Bind = nameof(FileSizeStringBind);
            }

            public FileUploadPage ParentPage => this.Parent.Parent as FileUploadPage;

            public string FileSizeStringBind => this.ParentPage.GetFileSizeString(this.FileSize);

            void Handle(Input.DeleteTrigger action)
            {
                if (File.Exists(this.FilePath))
                {
                    File.Delete(this.FilePath);
                }

                this.ParentPage.Files.Remove(this);
            }
        }

        [FileUploadPage_json.Tasks]
        partial class FileUploadTaskPage : Json
        {
            static FileUploadTaskPage()
            {
                DefaultTemplate.FileSizeString.Bind = "FileSizeStringBind";
            }

            public FileUploadPage ParentPage => this.Parent.Parent as FileUploadPage;

            public string FileSizeStringBind => this.ParentPage.GetFileSizeString(this.FileSize);
        }
    }
}