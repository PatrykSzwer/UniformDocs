using Starcounter;
using System.Collections.Generic;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemGroupPage : Json
    {
        protected override void OnData()
        {
            base.OnData();
        }

        static FormItemGroupPage()
        {
        }

        public string CityValidationMessage
        {
            get
            {
                switch (IsCityInvalid)
                {
                    case "false":
                        return "City is provided";
                    case "true":
                        return "City cannot be empty";
                }
                return "";
            }
        }

        public string PostcodeValidationMessage
        {
            get
            {
                switch (IsPostcodeInvalid)
                {
                    case "false":
                        return "Postcode is provided";
                    case "true":
                        return "Postcode cannot be empty";
                }
                return "";
            }
        }

        public string GroupValidationMessage
        {
            get
            {
                switch (IsGroupInvalid)
                {
                    case "false":
                        return "This is the expected pair of input!";
                    case "true":
                        return "The fields do not contain the expected pair of input";
                }
                return "Expecting 'Stockholm' and '12345'";
            }
        }

        public string IsCityInvalid
        {
            get
            {
                if (City.Length > 0)
                {
                    return "false";
                }
                return "true";
            }
        }

        public string IsPostcodeInvalid
        {
            get
            {
                if (Postcode.Length > 0)
                {
                    return "false";
                }
                return "true";
            }
        }

        public string IsGroupInvalid
        {
            get
            {
                if (City.Length > 0 && Postcode.Length > 0)
                {
                    if (City.ToLower().Equals("stockholm") && Postcode.Equals("12345"))
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