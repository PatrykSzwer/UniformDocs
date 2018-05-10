using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class UrlPage : Json
{
    protected override void OnData()
    {
        base.OnData();

        this.Url = "/KitchenSink";
        this.Label = "This a sample link";
    }
}
}