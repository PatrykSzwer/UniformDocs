using Starcounter;
using System;

namespace UniformDocs.ViewModels.Components
{
    partial class IntegerPage : Json
    {
        static IntegerPage()
        {
            DefaultTemplate.AgeReaction.Bind = nameof(CalculatedAgeReaction);
        }

        public string CalculatedAgeReaction
        {
            get
            {
                DateTime today = DateTime.Today;
                long birthYear = today.Year - Age - 1;
                return "You were born in " + birthYear;
            }
        }
    }
}