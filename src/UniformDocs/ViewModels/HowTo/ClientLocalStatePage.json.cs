using Starcounter;

namespace UniformDocs.ViewModels.HowTo
{
    partial class ClientLocalStatePage : Json
    {
        protected override void OnData()
        {
            base.OnData();

            var person = this.People.Add();
            person.Name = "John Doe";

            person = this.People.Add();
            person.Name = "Jessica Doe";
        }
    }
}