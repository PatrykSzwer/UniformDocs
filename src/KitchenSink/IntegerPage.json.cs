using Starcounter;
using System;

namespace KitchenSink {
  partial class IntegerPage : Partial {

    public string CalculatedAgeReaction {
      get {
        DateTime today = DateTime.Today;
        long birthYear = today.Year - Age - 1;
        return "You were born in " + birthYear;
      }
    }
  }
}
