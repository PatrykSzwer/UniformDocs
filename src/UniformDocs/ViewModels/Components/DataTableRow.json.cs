using Starcounter;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTableRow : Json, IBound<TableRow>
    {
        public void Handle(Input.DeleteTrigger action)
        {
            this.Data.Delete();
        }
    }
}
