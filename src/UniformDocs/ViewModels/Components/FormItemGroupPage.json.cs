using Starcounter;
using System.Collections.Generic;

namespace UniformDocs.ViewModels.Components
{
    partial class FormItemGroupPage : Json
    {
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
                    default:
                        return "";
                }
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
                    default:
                        return "";
                }
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
                    default:
                        return "Expecting 'Stockholm' and '12345'";
                }
            }
        }

        public string IsCityInvalid
        {
            get
            {
                return City.Length > 0 ? "false" : "true";
            }
        }

        public string IsPostcodeInvalid
        {
            get
            {
                return Postcode.Length > 0 ? "false" : "true";
            }
        }

        public string IsGroupInvalid
        {
            get
            {
                if (City.Length > 0 && Postcode.Length > 0)
                {
                    return City.ToLower().Equals("stockholm") && Postcode.Equals("12345") ? "false" : "true";
                }

                return "";
            }
        }

    }
}