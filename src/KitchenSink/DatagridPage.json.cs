using Starcounter;

namespace KitchenSink {
    partial class DatagridPage : Page {
        protected override void OnData() {
            base.OnData();

            DatagridPagePetsElementJson pet;
            pet = this.Pets.Add();
            pet.Name = "Rocky";
            pet.Kind = "Dog";

            pet = this.Pets.Add();
            pet.Name = "Tigger";
            pet.Kind = "Cat";

            pet = this.Pets.Add();
            pet.Name = "Bella";
            pet.Kind = "Rabbit";
        }

        void Handle(Input.AddPet action) {
            var p = Pets.Add();
            p.Name = "Cecil";
            p.Kind = "Hamster";
        }
    }

    [DatagridPage_json.Pets]
    partial class DatagridPagePetsElementJson : Json {

        public string CalculatedSound {
            get {
                switch (Kind) {
                    case "Dog":
                        return "Woof";

                    case "Cat":
                        return "Meow";

                    case "Rabbit":
                        return "Jump";

                    case "Hamster":
                        return "Squeak";

                    default:
                        return "";
                }
            }
        }
    }
}
