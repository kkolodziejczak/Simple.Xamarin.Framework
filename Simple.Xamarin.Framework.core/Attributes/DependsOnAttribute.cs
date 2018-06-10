using System;
using System.Collections.Generic;

namespace Simple.Xamarin.Framework.core
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
