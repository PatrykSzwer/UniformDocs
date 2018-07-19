using Starcounter;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTableRow : Json, IBound<Person>
    {
        public void Handle(Input.DeleteTrigger action)
        {
            this.Data.Delete();
            var rows = this.Parent as Arr<Json>;
            rows.Remove(this);
        }
    }
}
