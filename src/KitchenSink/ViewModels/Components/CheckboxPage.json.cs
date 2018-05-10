using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class CheckboxPage : Json
    {
        static CheckboxPage()
        {
            DefaultTemplate.DrivingLicenseReaction.Bind = nameof(CalculatedDrivingLicenseReaction);
        }

        public string CalculatedDrivingLicenseReaction
        {
            get
            {
                if (DrivingLicense == true)
                {
                    return "You can drive";
                }
                else
                {
                    return "You can't drive";
                }
            }
        }
    }
}