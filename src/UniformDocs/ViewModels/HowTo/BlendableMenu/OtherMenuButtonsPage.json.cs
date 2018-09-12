using Starcounter;
using Starcounter.Advanced;
using System;

namespace UniformDocs.ViewModels.HowTo.BlendableMenu
{
    partial class OtherMenuButtonsPage : Json
    {
        void Handle(Input.PayByCardTrigger action)
        {
            var blendingInfosDictionary = Blender.ListAllByUris();
            Console.WriteLine(blendingInfosDictionary.Count);
            //OnBuyReaction = action.Value == 0 ? Template.CarrotsReaction.DefaultValue : "You bought a carrot!";
        }
        void Handle(Input.PayByTransferTrigger action)
        {
            var blendingInfosDictionary = Blender.ListAllByUris();
            Console.WriteLine(blendingInfosDictionary.Count);
            //OnBuyReaction = action.Value == 0 ? Template.CarrotsReaction.DefaultValue : "You bought a carrot!";
        }
    }
}
