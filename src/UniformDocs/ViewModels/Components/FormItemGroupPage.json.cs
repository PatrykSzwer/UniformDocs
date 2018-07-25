using Starcounter;
using Starcounter.Uniform.FormItem;
using Starcounter.Uniform.Generic.FormItem;
using Starcounter.Uniform.ViewModels;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemGroupPage : Json
    {
        private string GroupPropertyName => "CityPostcodeGroup";
        static FormItemGroupPage()
        {
            DefaultTemplate.FormItemMetadata.InstanceType = typeof(FormItemMetadata);
        }

        public void Init()
        {
            this.FormItemMetadata = new FormItemMessagesBuilder().ForProperties(new[] { nameof(this.City), nameof(this.Postcode), this.GroupPropertyName }).Build();
            ValidateGroup();
        }

        void Handle(Input.City action)
        {
            this.City = action.Value;

            if (this.City.Length > 0)
            {
                this.FormItemMetadata.SetMessage(nameof(this.City), "City is provided.", MessageType.Valid);
            }
            else
            {
                this.FormItemMetadata.SetMessage(nameof(this.City), "City cannot be empty!", MessageType.Invalid);
            }

            ValidateGroup();
        }

        void Handle(Input.Postcode action)
        {
            this.Postcode = action.Value;

            if (this.Postcode.Length > 0)
            {
                this.FormItemMetadata.SetMessage(nameof(this.Postcode), "Postcode is provided.", MessageType.Valid);
            }
            else
            {
                this.FormItemMetadata.SetMessage(nameof(this.Postcode), "Postcode cannot be empty!", MessageType.Invalid);
            }

            ValidateGroup();
        }

        private void ValidateGroup()
        {
            if (this.City.Length == 0 || this.Postcode.Length == 0)
            {
                this.FormItemMetadata.SetMessage(this.GroupPropertyName, "Expecting 'Stockholm' and '12345'", MessageType.Neutral);
            }
            else if (this.City.ToLower().Equals("stockholm") && this.Postcode.Equals("12345"))
            {
                this.FormItemMetadata.SetMessage(this.GroupPropertyName, "This is the expected pair of input!", MessageType.Valid);
            }
            else
            {
                this.FormItemMetadata.SetMessage(this.GroupPropertyName, "The fields do not contain the expected pair of input!", MessageType.Invalid);
            }
        }
    }
}