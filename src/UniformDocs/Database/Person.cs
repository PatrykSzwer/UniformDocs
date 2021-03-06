﻿using Starcounter;

namespace UniformDocs.Database
{
    // Database class used by DataTable page.
    [Database]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}