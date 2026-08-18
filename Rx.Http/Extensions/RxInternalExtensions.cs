using System.Collections.Generic;

namespace Rx.Http.Extensions
{
    internal static class RxInternalExtensions
    {
        internal static IDictionary<string, string> ToDictionary(this object obj)
        {
            var keys = new Dictionary<string, string>();

            var type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                var value = field.GetValue(obj);
                if (value != null)
                {
                    keys[field.Name] = value.ToString();
                }
            }

            foreach (var property in type.GetProperties())
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    keys[property.Name] = value.ToString();
                }
            }

            return keys;
        }
    }
}
