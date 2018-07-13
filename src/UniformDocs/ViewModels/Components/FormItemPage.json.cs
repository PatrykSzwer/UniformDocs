using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
        }

        static FormItemPage()
        {
        }

        public string ValidationMessage
        {
            get {
                switch(IsInvalid)
                {
                    case "false":
                        return "Correct greeting!";
                    case "true":
                        return "This is not the correct greeting";
                }
                return "'Hello' is the only accepted value";
            }
        }

        public string IsInvalid 
        {
            get
            {
                if (Word.Length > 0)
                {
                    if (Word.ToLower().Equals("hello"))
                    {
                        return "false";
                    }
                    return "true";
                }
                return "";
            }
        }
    }
}