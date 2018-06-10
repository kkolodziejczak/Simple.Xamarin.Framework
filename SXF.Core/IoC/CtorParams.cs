using System.Collections.Generic;

namespace SXF.Core.IoC
{

    /// <summary>
    /// Class that represents parameters that can be passed to constructor via IoC Constructor injection.
    /// </summary>
    /// <exception cref="ParameterNotFoundException">Thrown when there is no key in parameters collection.</exception>
    public class CtorParams
    {
        public Dictionary<string, object> Parameters { get; protected set; }

        public CtorParams(params (string key, object value)[] parameters)
        {
            Parameters = new Dictionary<string, object>();
            foreach (var (key, value) in parameters)
            {
                Parameters.Add(key, value);
            }
        }

        public T Value<T>(string key)
        {
            if (!Parameters.ContainsKey(key))
            {
                throw new ParameterNotFoundException(key);
            }
            return (T)Parameters[key];
        }

    }
}
