using Starcounter;
using System.Threading;

namespace UniformDocs.ViewModels.Components
{
    partial class ButtonPage : Json
    {
        void Handle(Input.AddCarrotsTrigger action)
        {
            if (action.Value == 0)
            {
                CarrotsReaction = Template.CarrotsReaction.DefaultValue;
            }
            else
            {
                CarrotsReaction = "You have " + action.Value + " imaginary carrots";
            }
        }

        void Handle(Input.EnableCarrotEngine action)
        {
            if (action.Value == false)
            {
                CarrotEngineReaction = Template.CarrotEngineReaction.DefaultValue;
            }
            else
            {
                CarrotEngineReaction = "Carrot engine is on";
            }
        }

        void Handle(Input.BuyCarrotTrigger action)
        {
            OnBuyReaction = action.Value == 0 ? Template.CarrotsReaction.DefaultValue : "You bought a carrot!";
        }

        void Handle(Input.AddOneCarrotTrigger action)
        {
            if (action.Value == 0)
            {
                OneCarrotReaction = Template.OneCarrotReaction.DefaultValue;
            }
            else
            {
                OneCarrotReaction = "You have " + action.Value + " imaginary carrots";
                AddOneCarrotDisabled = true;
            }
        }

        void Handle(Input.TakeOneRegeneratingCarrotTrigger action)
        {
            Thread.Sleep(5000);
            action.Cancel();
        }
    }
}