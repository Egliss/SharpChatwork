using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpChatwork.Client
{
    class URLArgEncoder
    {
        public static Dictionary<string, string> FromData<T>(T input)
        {
            return typeof(T).GetMembers()
                .Where(m => m.MemberType == MemberTypes.Field)
                .Where(m => m.DeclaringType.IsPublic)
                .Select(m => new KeyValuePair<string, object>(m.Name, typeof(T).GetField(m.Name).GetValue(input)))
                .Where(m => m.Value != null)
                .Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString()))
                .Concat(
                    typeof(T).GetMembers()
                    .Where(m => m.MemberType == MemberTypes.Property)
                    .Where(m => m.DeclaringType.IsPublic)
                    .Select(m => new KeyValuePair<string, object>(m.Name, typeof(T).GetProperty(m.Name).GetValue(input)))
                    .Where(m => m.Value != null)
                    .Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString())))
                .ToDictionary(m => m.Key, n => n.Value);
        }
    }
}
