using Starcounter;
using Starcounter.XSON.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UniformDocs
{
    public class ServiceWorkerMiddleWare : IMiddleware
    {
        static Encoding defaultEncoding = Encoding.UTF8;
        string appShell, serviceWorkerTemplate;
        string RUNTIME_CACHE_KEY = Guid.NewGuid().ToString();

        /// <summary>
        /// Creates a new instance of <see cref="ServiceWorkerMiddleWare"/>
        /// using the template fetched from the static file server.
        /// </summary>
        public ServiceWorkerMiddleWare()
        {
           
        }

        /// <summary>
        /// Creates a new instance of <see cref="ServiceWorkerMiddleWare"/>
        /// using the given standalone page template.
        /// </summary>
        public ServiceWorkerMiddleWare(string standaloneTemplate)
        {
            if (string.IsNullOrEmpty(standaloneTemplate)) throw new ArgumentNullException("standaloneTemplate");
            appShell = standaloneTemplate;
        }

        void IMiddleware.Register(Application application)
        {
            application.Use(request =>
            {
                if (request.Uri.Equals("/sw.js"))
                {
                    string template;
                    if (!string.IsNullOrEmpty(serviceWorkerTemplate))
                    {
                        template = serviceWorkerTemplate;
                    }
                    else
                    {
                        template = FetchServiceWorkerTemplate();
                        serviceWorkerTemplate = template;
                    }
                    var SW = template.Replace("REPLACE_ME_WTH_RUNTIME_HASH", RUNTIME_CACHE_KEY);
                    var response = new Response
                    {
                        BodyBytes = defaultEncoding.GetBytes(SW)
                    };
                    response.Headers["Content-Type"] = "text/javascript";
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
                string appShellHTMLUrl = "/sys/app-shell/app-shell-with-service-worker.html";
                return Self.GET(appShellHTMLUrl).Body;
            }
            catch
            {
                throw new Exception(@"Could not fetch /sys/app-shell/app-shell-with-service-worker.html");
            }
        }

        private static string FetchServiceWorkerTemplate()
        {
            try
            {
                string serviceWorkerUrl = "/service-worker.js";
                return Self.GET(serviceWorkerUrl).Body;
            }
            catch
            {
                throw new Exception(@"Could not fetch /service-worker.js");
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