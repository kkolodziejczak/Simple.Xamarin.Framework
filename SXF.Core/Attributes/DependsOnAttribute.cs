using System;
using System.Collections.Generic;

namespace SXF.Core
{
    public class DependsOnAttribute : Attribute
    {
        public IEnumerable<string> SourceProperties { get; set; }

        public DependsOnAttribute(params string[] propertyNames)
        {
            SourceProperties = propertyNames;
        }

    }
}
