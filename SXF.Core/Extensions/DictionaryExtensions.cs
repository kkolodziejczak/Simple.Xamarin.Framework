using System.Collections.Generic;

namespace SXF.Core
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns true if <see cref="Dictionary{TKey, TValue}"/> doesn't contains <see cref="key"/>
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool NotContainsKey<Tkey, TValue>(this Dictionary<Tkey, TValue> source, string key)
        {
            foreach (var pair in source)
            {
                if (source.ContainsKey(pair.Key))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
