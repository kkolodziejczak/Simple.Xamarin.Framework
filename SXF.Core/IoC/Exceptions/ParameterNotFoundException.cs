using System;

namespace SXF.Core.IoC
{
    public class ParameterNotFoundException : Exception
    {
        public string KeyName { get; private set; }

        public ParameterNotFoundException(string key) : base()
        {
            KeyName = key;
        }
    }
}
