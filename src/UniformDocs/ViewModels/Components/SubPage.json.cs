using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class SubPage : Json
    {
        public void Handle(Input.ButtonTrigger action)
        {
            this.DeepSub.MessageSub = "Marcin2";
        }
    }
}
