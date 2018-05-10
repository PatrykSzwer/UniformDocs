using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class TextareaPage : Json
    {
        static TextareaPage()
        {
            DefaultTemplate.BioReaction.Bind = nameof(CalculatedBioReaction);
        }

        public string CalculatedBioReaction
        {
            get { return "Length of your bio: " + Bio.Length + " chars"; }
        }
    }
}