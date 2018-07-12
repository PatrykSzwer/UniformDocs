using System;

namespace UniformDocs.Database
{
    public class Country
    {
        public Country(string name, string capitalName)
        {
            Name = name;
            CapitalName = capitalName;
        }

        public string Name { get; set; }
        public string CapitalName { get; set; }
    }
}