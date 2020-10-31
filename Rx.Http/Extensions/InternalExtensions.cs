using System.Collections.Generic;

namespace Rx.Http.Extensions
{
    internal static class InternalExtensions
    {
        internal static IDictionary<string, string> ToDictionary(this object obj)
        {
            var keys = new Dictionary<string, string>();

            var type = obj.GetType();

            foreach (var field in type.GetFields())
            {
                keys.Add(field.Name, field.GetValue(obj).ToString());
            }

            foreach (var property in type.GetProperties())
            {
                keys.Add(property.Name, property.GetValue(obj).ToString());
            }

            return keys;
        }
    }
}
