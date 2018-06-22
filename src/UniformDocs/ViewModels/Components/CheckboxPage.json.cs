using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class CheckboxPage : Json
    {
        static CheckboxPage()
        {
            DefaultTemplate.DrivingLicenseReaction.Bind = nameof(CalculatedDrivingLicenseReaction);
        }

        public string CalculatedDrivingLicenseReaction => DrivingLicense ? "You can drive" : "You can't drive";
    }
}