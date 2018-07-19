using Starcounter;
using Starcounter.Uniform.Generic.FormItem;
using Starcounter.Uniform.ViewModels;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemPage : Json
    {
        static FormItemPage()
        {
            DefaultTemplate.ItemMessages.InstanceType = typeof(ItemMessages);
        }

        public void Init()
        {
            this.ItemMessages = new FormItemMessagesBuilder().ForProperty(nameof(FormItemPage.Word)).Build();
            this.ItemMessages.SetMessage(nameof(this.Word), "'Hello' is the only accepted value", MessageType.Neutral);
        }

        void Handle(Input.Word action)
        {
            if (action.Value.Length > 0)
            {
                if (action.Value.ToLower().Equals("hello"))
                {
                    this.ItemMessages.SetMessage(nameof(this.Word), "Correct greeting!", MessageType.Valid);
                }
                else
                {
                    this.ItemMessages.SetMessage(nameof(this.Word), "This is not the correct greeting!", MessageType.Invalid);
                }
            }
            else
            {
                this.ItemMessages.SetMessage(nameof(this.Word), "'Hello' is the only accepted value", MessageType.Neutral);
            }
        }
    }
}