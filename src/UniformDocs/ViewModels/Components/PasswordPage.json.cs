using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class PasswordPage : Json
    {
        static PasswordPage()
        {
            DefaultTemplate.PasswordReaction.Bind = nameof(CalculatedPasswordReaction);
        }

        public string CalculatedPasswordReaction => Password.Length < 6 ? "Password must be at least 6 chars long" : "Good password!";
    }
}