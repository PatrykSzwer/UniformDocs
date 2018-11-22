using Starcounter;
using System;
using UniformDocs.Database;

namespace UniformDocs.ViewModels.Components
{
    partial class DataTableRow : Json, IBound<Person>
    {
        public Action<DataTableRow> DeleteAction { get; set; }

        public void Handle(Input.DeleteTrigger action)
        {
            //this.DeleteAction?.Invoke(this);
            this.Deep.Message = "Marcin1";
        }
    }
}
