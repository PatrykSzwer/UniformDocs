using Starcounter;

namespace UniformDocs.ViewModels.HowTo
{
    partial class DialogPage : Json
    {
        void Handle(Input.OpenTrigger action)
        {
            action.Cancel();

            this.Opened = true;
        }

        void Handle(Input.ConfirmTrigger action)
        {
            this.Opened = false;
            this.Message = "You have accepted the dialog box";
            this.MessageType = "success";
        }

        void Handle(Input.RejectTrigger action)
        {
            this.Opened = false;
            this.Message = "You have rejected the dialog box";
            this.MessageType = "danger";
        }
    }
}
