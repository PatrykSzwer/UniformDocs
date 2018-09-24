using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class UrlPage : Json
    {
        public string DownloadLinkSessionId => Session.Current.SessionId;

        protected override void OnData()
        {
            base.OnData();

            this.Url = "/UniformDocs";
            this.Label = "This a sample link";
        }

        public void DownloadStarted()
        {
            this.DownloadLinkFeedback = "Your download has started";
        }
    }
}