using System.Collections.Generic;

namespace Rx.Http
{
    public sealed class ListDictionary<TKey, TValue> : Dictionary<TKey, List<TValue>>
    {
        public ListDictionary<TKey, TValue> Append(TKey key, TValue value)
        {
            if (ContainsKey(key))
            {
                this[key].Add(value);
            }
            else
            {
                Add(key, new List<TValue> { value });
            }
            return this;
        }
    }
}
