using System;
using System.Threading.Tasks;
using Starcounter;
using Starcounter.Async;

namespace UniformDocs.ViewModels.HowTo
{
    partial class FlashMessagePage : Json
    {
        public void Handle(Input.ShowMessageTrigger action)
        {
            AsyncInputHandlers.Run(async () =>
            {
                this.ServerMessage = "This Message was set on the Server side!";
                await Task.Delay(TimeSpan.FromSeconds(3));
                this.ServerMessage = null;
            });
        }
    }
}
