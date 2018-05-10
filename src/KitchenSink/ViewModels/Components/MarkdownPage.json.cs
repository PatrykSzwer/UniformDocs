using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class MarkdownPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Bio = @"# This is a structured text

It supports **markdown** *syntax*.";
        }
    }
}