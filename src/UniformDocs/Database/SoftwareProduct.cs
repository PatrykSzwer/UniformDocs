﻿using Starcounter;

namespace UniformDocs.Database
{
    // Database class used by Dropdown page.
    [Database]
    public class SoftwareProduct
    {
        public string Name { get; set; }
        public string Key => this.GetObjectID();
    }
}
