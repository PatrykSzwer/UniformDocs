using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
        }

        static FormItemPage()
        {
            DefaultTemplate.IsInvalid.Bind = nameof(CalulateIsInvalid);
            DefaultTemplate.ValidationMessage.Bind = nameof(CalulateValidationMessage);
        }

        public string CalulateValidationMessage => CalulateIsInvalid ? "Not accepted" : "Accepted";
        public bool CalulateIsInvalid => this.Word != "Hello";
    }
}