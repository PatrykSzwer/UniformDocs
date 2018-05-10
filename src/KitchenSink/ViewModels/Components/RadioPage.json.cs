using Starcounter;

namespace KitchenSink.ViewModels.Components
{
    partial class RadioPage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            var pet = this.Pets.Add();
            pet.Label = "dogs";

            pet = this.Pets.Add();
            pet.Label = "cats";

            pet = this.Pets.Add();
            pet.Label = "rabbit";

            this.SelectedPet = "dogs";
        }

        static RadioPage()
        {
            DefaultTemplate.PetReaction.Bind = nameof(CalculatedPetReaction);
        }

        public string CalculatedPetReaction => "You like " + SelectedPet;
    }
}