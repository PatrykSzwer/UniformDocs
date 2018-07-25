using Starcounter;
using Starcounter.Uniform.FormItem;
using Starcounter.Uniform.Generic.FormItem;
using Starcounter.Uniform.ViewModels;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemPage : Json
    {
        static FormItemPage()
        {
            DefaultTemplate.FormItemMetadata.InstanceType = typeof(FormItemMetadata);
        }

        public void Init()
        {
            this.FormItemMetadata = new FormItemMessagesBuilder().ForProperty(nameof(this.Word)).Build();
            this.FormItemMetadata.SetMessage(nameof(this.Word), "'Hello' is the only accepted value", MessageType.Neutral);
        }

        void Handle(Input.Word action)
        {
            if (action.Value.Length > 0)
            {
                if (action.Value.ToLower().Equals("hello"))
                {
                    this.FormItemMetadata.SetMessage(nameof(this.Word), "Correct greeting!", MessageType.Valid);
                }
                else
                {
                    this.FormItemMetadata.SetMessage(nameof(this.Word), "This is not the correct greeting!", MessageType.Invalid);
                }
            }
            else
            {
                this.FormItemMetadata.SetMessage(nameof(this.Word), "'Hello' is the only accepted value", MessageType.Neutral);
            }
        }
    }
}