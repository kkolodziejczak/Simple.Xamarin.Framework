using System;

namespace SXF.Core.IoC
{
    public class IoCKeyNotFoundException : Exception
    {
        public IoCKeyNotFoundException(string message) : base(message)
        {
        }
    }
}
