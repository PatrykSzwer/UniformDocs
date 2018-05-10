using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class DialogPage : Json
    {
        void Handle(Input.OpenTrigger Action)
        {
            Action.Cancel();

            this.Opened = true;
        }

        void Handle(Input.ConfirmTrigger Action)
        {
            this.Opened = false;
            this.Message = "You have accepted the dialog box";
            this.MessageType = "success";
        }

        void Handle(Input.RejectTrigger Action)
        {
            this.Opened = false;
            this.Message = "You have rejected the dialog box";
            this.MessageType = "danger";
        }
    }
}
