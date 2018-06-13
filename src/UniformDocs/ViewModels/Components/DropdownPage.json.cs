using System.Linq;
using UniformDocs.Database;
using Starcounter;

namespace UniformDocs.ViewModels.Components
{
    partial class DropdownPage : Json
    {
        static DropdownPage()
        {
            DefaultTemplate.SelectedProductKey.Bind = nameof(SelectedProductKeyBind);
            DefaultTemplate.PetReaction.Bind = nameof(CalculatedPetReaction);
        }

        protected override void OnData()
        {
            base.OnData();

            DropdownPage.PetsElementJson pet = this.Pets.Add();
            pet.Label = "dogs";

            pet = this.Pets.Add();
            pet.Label = "cats";

            pet = this.Pets.Add();
            pet.Label = "rabbit";

            this.SelectedPet = "dogs";

            this.Products.Data = Db.SQL("SELECT p FROM UniformDocs.Database.SoftwareProduct p ORDER BY p.Name");
            this.SelectedProduct.Data = Db.SQL("SELECT p FROM UniformDocs.Database.SoftwareProduct p WHERE p.Name = ?", "Starcounter Database").FirstOrDefault();
        }

        public string CalculatedPetReaction => $"You like { SelectedPet}";

        public string SelectedProductKeyBind
        {
            get => SelectedProduct?.Key;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.SelectedProduct.Data = null;
                    return;
                }

                this.SelectedProduct.Data = Db.FromId(DbHelper.Base64DecodeObjectID(value)) as SoftwareProduct;
            }
        }
    }
}