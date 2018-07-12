using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class UrlPage : Json
{
    protected override void OnData()
    {
        base.OnData();

        this.Url = "/UniformDocs";
        this.Label = "This a sample link";
    }
}
}