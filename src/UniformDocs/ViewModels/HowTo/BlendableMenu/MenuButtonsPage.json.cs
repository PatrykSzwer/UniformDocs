using Starcounter;

namespace UniformDocs.ViewModels.HowTo.BlendableMenu
{
    partial class MenuButtonsPage : Json
    {
        void Handle(Input.PayByCardTrigger action)
        {
            //OnBuyReaction = action.Value == 0 ? Template.CarrotsReaction.DefaultValue : "You bought a carrot!";
        }
    }
}
