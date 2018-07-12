using Starcounter;

namespace UniformDocs.ViewModels.HowTo
{
    partial class RedirectPage : Json
    {
        void Handle(Input.GoToHomePartialTrigger action)
        {
            this.MorphUrl = "/UniformDocs";
        }

        void Handle(Input.ChooseFood action)
        {
            switch (action.Value)
            {
                case "Fruit":
                    this.MorphUrl = "/UniformDocs/Redirect/apple";
                    break;

                case "Vegetable":
                    this.MorphUrl = "/UniformDocs/Redirect/carrot";
                    break;

                case "Bread":
                    this.MorphUrl = "/UniformDocs/Redirect/baguette";
                    break;
            }
        }

        void Handle(Input.GoToDocsTrigger action)
        {
            this.RedirectUrl = "https://starcounter.io/";
        }
    }
}