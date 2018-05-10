using System;
using Starcounter;

namespace KitchenSink.ViewModels.HowTo
{
    partial class CallbackPage : Page
    {
        protected int Timeout = 1000;

        protected override void OnData()
        {
            base.OnData();

            this.Items.Add();
            this.Items.Add();
            this.Items.Add();
            this.Items.Add();
        }

        protected void Handle(Input.SaveTrigger action)
        {
            action.Cancel();
            System.Threading.Thread.CurrentThread.Join(Timeout);
        }

        protected void Handle(Input.SaveAndSpinTrigger action)
        {
            action.Cancel();
            System.Threading.Thread.CurrentThread.Join(Timeout);
        }

        protected void Handle(Input.SaveAndMessageTrigger action)
        {
            action.Cancel();

            this.ErrorMessage = string.Empty;
            this.SuccessMessage = string.Empty;
            System.Threading.Thread.CurrentThread.Join(Timeout);

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
            protected int Timeout = 1000;

            protected void Handle(Input.SaveTrigger action)
            {
                action.Cancel();
                System.Threading.Thread.CurrentThread.Join(Timeout);
            }

            protected void Handle(Input.SaveAndSpinTrigger action)
            {
                action.Cancel();
                System.Threading.Thread.CurrentThread.Join(Timeout);
            }

            protected void Handle(Input.SaveAndMessageTrigger action)
            {
                action.Cancel();

                this.ErrorMessage = string.Empty;
                this.SuccessMessage = string.Empty;
                System.Threading.Thread.CurrentThread.Join(Timeout);

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