using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class DecimalPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            this.Price = 10;
        }

        static DecimalPage()
        {
            DefaultTemplate.PriceReaction.Bind = nameof(CalculatedPriceReaction);
        }

        public string CalculatedPriceReaction
        {
            get { return "5% of tax is " + (Price/20); }
        }
    }
}