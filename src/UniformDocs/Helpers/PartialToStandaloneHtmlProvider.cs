using Starcounter;
using Starcounter.XSON.Advanced;
using System;
using System.Text;

namespace UniformDocs
{
    public class PartialToStandaloneHtmlProvider : IMiddleware
    {
        static Encoding defaultEncoding = Encoding.UTF8;
        string appShell;

        /// <summary>
        /// Creates a new instance of <see cref="PartialToStandaloneHtmlProvider"/>
        /// using the template fetched from the static file server.
        /// </summary>
        public PartialToStandaloneHtmlProvider()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="PartialToStandaloneHtmlProvider"/>
        /// using the given standalone page template.
        /// </summary>
        public PartialToStandaloneHtmlProvider(string standaloneTemplate)
        {
            if (string.IsNullOrEmpty(standaloneTemplate)) throw new ArgumentNullException("standaloneTemplate");
            appShell = standaloneTemplate;
        }

        void IMiddleware.Register(Application application)
        {
            application.Use(request =>
            {
                if (request.Uri.Equals("/service-worker.js"))
                {
                    var ServiceWorkerBodyBytes = ServiceWorkerBodyBytesSingleton.Instance.GetBodyBytes();
                    var response = new Response
                    {
                        BodyBytes = ServiceWorkerBodyBytes
                    };
                    response.Headers["Content-Type"] = "application/javascript";
                    request.SendResponse(response);
                }
                return null;
            });

            application.Use(MimeProvider.Html(this.Invoke));
        }

        private static string FetchAppShellTemplate()
        {
            try
            {
                string appShellHTMLUrl = "/sys/app-shell/app-shell.html";
                return Self.GET(appShellHTMLUrl).Body;
            }
            catch
            {
                throw new Exception(@"Could not fetch /sys/app-shell/app-shell.html");
            }
        }
             

        void Invoke(MimeProviderContext context, Action next)
        {
            var content = context.Result;

            if (content != null)
            {
                var json = context.Resource as Json;
                if (json != null)
                {
                    Session session = Session.Ensure();
                    session.SetClientRoot(json);

                    if (!IsFullPageHtml(content))
                    {
                        content = ProvideImplicitStandalonePage(content, context.Request.HandlerAppName, session.SessionUri);
                        context.Result = content;
                    }
                }
            }

            next();
        }

        internal byte[] ProvideImplicitStandalonePage(byte[] content, string appName, string sessionUri)
        {
            string template;
            if (!string.IsNullOrEmpty(appShell))
            {
                template = appShell;
            }
            else
            {
                appShell = FetchAppShellTemplate();
                template = appShell;
            }
            var html = String.Format(template, appName, sessionUri);
            return defaultEncoding.GetBytes(html);
        }

        internal static bool IsFullPageHtml(Byte[] html)
        {
            // This method is just copied here from obsolete Partial class, as is. It should
            // be reviewed and probably improved, or alternatively redesigned.

            //TODO test for UTF-8 BOM
            byte[] fullPageTest = defaultEncoding.GetBytes("<!"); //full page starts with <!doctype or <!DOCTYPE;
            var indicatorLength = fullPageTest.Length;

            if (html.Length < indicatorLength)
            {
                return false; // this is too short for a full html
            }

            for (var i = 0; i < indicatorLength; i++)
            {
                if (html[i] == fullPageTest[i])
                {
                    continue;
                }
                return false; //it's a partial
            }

            return true; //it's a full html
        }
    }
}