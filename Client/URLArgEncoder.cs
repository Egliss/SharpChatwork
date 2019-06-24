using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharpChatwork.Client
{
    class URLArgEncoder
    {
        public static Dictionary<string, string> ToDictionary<T>(T input)
        {
            var type = typeof(T);
            return type.GetMembers()
                .Where(m => m.MemberType == MemberTypes.Field)
                .Where(m => m.DeclaringType.IsPublic)
                .Select(m => new KeyValuePair<string, object>(m.Name, type.GetField(m.Name).GetValue(input)))
                .Where(m => m.Value != null)
                .Where(m=> !string.IsNullOrEmpty(m.Value.ToString()))
                .Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString()))
                .Concat(
                    type.GetProperties()
                    .Where(m => m.MemberType == MemberTypes.Property)
                    .Where(m => m.DeclaringType.IsPublic)
                    .Where(m => type.GetProperty(m.Name).CanRead)
                    .Select(m => new KeyValuePair<string, object>(m.Name, type.GetProperty(m.Name).GetValue(input)))
                    .Where(m => m.Value != null)
                    .Where(m=> !string.IsNullOrEmpty(m.Value.ToString()))
                    .Select(m => new KeyValuePair<string, string>(m.Key, m.Value.ToString())))
                .ToDictionary(m => m.Key, n => n.Value);
        }
        public static string ToURLArg<T>(T input)
        {
            return "?" + string.Join("^&" , ToDictionary(input).Select(m => $"{m.Key}={m.Value}"));
        }
    }
}
