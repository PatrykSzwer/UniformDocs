using System;
using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    //normally this is be a [Database] class
    public class AnyData
    {
        public string Name = "Marcin";
    }

    partial class NestedPartial : Json, IBound<AnyData>
    {
        void Handle(Input.AddChildTrigger action)
        {
            var newNestedChild = Self.GET<NestedPartial>("/KitchenSink/partial/nested");
            newNestedChild.Html = DefaultTemplate.Html.DefaultValue + "?" + DateTime.Now;   //normally you don't need this
            this.ChildPartial = newNestedChild;
        }
    }
}