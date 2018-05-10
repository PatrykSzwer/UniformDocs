using System;
using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class CallbackPage : Page
    {
        protected int timeout = 1000;

        protected override void OnData()
        {
            base.OnData();

            this.Items.Add();
            this.Items.Add();
            this.Items.Add();
            this.Items.Add();
        }

        protected void Handle(Input.SaveTrigger Action)
        {
            Action.Cancel();
            System.Threading.Thread.CurrentThread.Join(timeout);
        }

        protected void Handle(Input.SaveAndSpinTrigger Action)
        {
            Action.Cancel();
            System.Threading.Thread.CurrentThread.Join(timeout);
        }

        protected void Handle(Input.SaveAndMessageTrigger Action)
        {
            Action.Cancel();

            this.ErrorMessage = string.Empty;
            this.SuccessMessage = string.Empty;
            System.Threading.Thread.CurrentThread.Join(timeout);

            if (DateTime.Now.Ticks % 2 == 0)
            {
                this.SuccessMessage = "This is a random success alert from the server";
            }
            else
            {
                this.ErrorMessage = "This is a random danger alert from the server";
            }
        }

        [CallbackPage_json.Items]
        partial class CallbackPageItem
        {
            protected int timeout = 1000;

            protected void Handle(Input.SaveTrigger Action)
            {
                Action.Cancel();
                System.Threading.Thread.CurrentThread.Join(timeout);
            }

            protected void Handle(Input.SaveAndSpinTrigger Action)
            {
                Action.Cancel();
                System.Threading.Thread.CurrentThread.Join(timeout);
            }

            protected void Handle(Input.SaveAndMessageTrigger Action)
            {
                Action.Cancel();

                this.ErrorMessage = string.Empty;
                this.SuccessMessage = string.Empty;
                System.Threading.Thread.CurrentThread.Join(timeout);

                if (DateTime.Now.Ticks % 2 == 0)
                {
                    this.SuccessMessage = "The changes are successfully saved";
                }
                else
                {
                    this.ErrorMessage = "Failed to save changes";
                }
            }
        }
    }
}