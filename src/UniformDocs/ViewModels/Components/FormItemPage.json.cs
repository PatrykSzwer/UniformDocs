using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemPage : Json
    {
        public string ValidationMessage
        {
            get {
                switch(IsInvalid)
                {
                    case "false":
                        return "Correct greeting!";
                    case "true":
                        return "This is not the correct greeting";
                    default:
                        return "'Hello' is the only accepted value";
                }
            }
        }

        public string IsInvalid 
        {
            get
            {
                if (Word.Length > 0)
                {
                    return Word.ToLower().Equals("hello") ? "false" : "true";
                }

                return "";
            }
        }
    }
}