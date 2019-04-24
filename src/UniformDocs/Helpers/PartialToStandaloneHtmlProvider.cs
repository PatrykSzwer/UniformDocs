using System;
using System.Text;
using Starcounter.XSON.Advanced;
using Starcounter;

namespace UniformDocs
{
    /// <summary>
    /// For any HTTP request with `Accept: text/html` with a response that is HTML,
    /// checks if the HTML document is a partial (does not start with a doctype).
    /// If yes, returns the app shell that contains code to bootstrap
    /// a Palindrom connection instead of that HTML.
    ///
    /// The HTML template cointans the session URL that was returned from the handler,
    /// so that Palindrom can request the relevant JSON in a following request.
    ///
    /// Must be used after `HtmlFromJsonProvider` middleware.
    ///
    /// A custom HTML template can be provided as a string parameter to the constructor.
    ///
    /// Middleware only wraps requests that have a HTTP handler. Since this middleware
    /// wraps the actual response, it will also have the HTTP status code
    /// that was returned from the handler.
    /// </summary>
    public class PartialToStandaloneHtmlProvider : IMiddleware
    {
        static Encoding defaultEncoding = Encoding.UTF8;

        string appShellPreconfigured;

        private static string FetchTemplate()
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
            appShellPreconfigured = standaloneTemplate;
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
            string template = appShellPreconfigured ?? FetchTemplate();
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
