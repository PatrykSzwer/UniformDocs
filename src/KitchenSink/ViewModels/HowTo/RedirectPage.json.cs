using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class RedirectPage : Json
    {
        void Handle(Input.GoToHomePartialTrigger action)
        {
            this.MorphUrl = "/KitchenSink";
        }

        void Handle(Input.ChooseFood action)
        {
            switch (action.Value)
            {
                case "Fruit":
                    this.MorphUrl = "/KitchenSink/Redirect/apple";
                    break;

                case "Vegetable":
                    this.MorphUrl = "/KitchenSink/Redirect/carrot";
                    break;

                case "Bread":
                    this.MorphUrl = "/KitchenSink/Redirect/baguette";
                    break;
            }
        }

        void Handle(Input.GoToDocsTrigger action)
        {
            this.RedirectUrl = "https://starcounter.io/";
        }
    }
}