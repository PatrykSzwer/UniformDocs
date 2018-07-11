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
            DefaultTemplate.IsInvalid.Bind = nameof(CalulateIsInvalidAddress);
            DefaultTemplate.ValidationMessage.Bind = nameof(CalulateValidationMessage);
        }

        public string CalulateValidationMessage { get {
                var invalids = new List<string>();
                if(CalulateIsInvalidCity)
                {
                    invalids.Add("city");
                }
                if (CalulateIsInvalidPostcode)
                {
                    invalids.Add("postcode");
                }
                if (invalids.Count > 0)
                {
                    var message = string.Join(" and ", invalids); // eg "City" or "City and Postcode" or "Postcode"
                    message += invalids.Count == 2 ? " are " : " is ";
                    message += "invalid";
                    message = message[0].ToString().ToUpper() + message.Remove(0, 1); // capitilize first letter
                    return message;
                } else
                {
                    return "Correct address 👍!";
                }
            }
        }

        public bool CalulateIsInvalidCity => this.City != "Stockholm";
        public bool CalulateIsInvalidPostcode => this.Postcode != "12345";
        public bool CalulateIsInvalidAddress => CalulateIsInvalidCity || CalulateIsInvalidPostcode;

    }
}