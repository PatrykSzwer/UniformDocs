using Starcounter;
using Starcounter.Advanced;
using System;

namespace UniformDocs.ViewModels.HowTo.BlendableMenu
{
    partial class MenuButtonsPage : Json
    {
        void Handle(Input.PayByCardTrigger action)
        {
            var blendingInfosDictionary = Blender.ListAllByUris();
            Console.WriteLine(blendingInfosDictionary.Count);
            //OnBuyReaction = action.Value == 0 ? Template.CarrotsReaction.DefaultValue : "You bought a carrot!";
        }
    }
}
