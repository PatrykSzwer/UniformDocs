using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class TextareaPage : Json
    {
        static TextareaPage()
        {
            DefaultTemplate.BioReaction.Bind = nameof(CalculatedBioReaction);
        }

        public string CalculatedBioReaction => "Length of your bio: " + Bio.Length + " chars";
    }
}