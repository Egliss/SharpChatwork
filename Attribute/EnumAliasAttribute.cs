using System;
using System.Linq;

namespace SharpChatwork
{
    internal class EnumAliasAttribute : Attribute
    {
        public string AliasName { get; set; }
        public EnumAliasAttribute(string aliasName)
        {
            this.AliasName = aliasName;
        }
    }
    internal static class EnumAliasExtension
    {
        public static string ToAliasOrDefault(this Enum value)
        {
            var i = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumAliasAttribute), false)
                .Cast<EnumAliasAttribute>()
                .FirstOrDefault();

            // use default name
            if (i == null)
                return Enum.GetName(value.GetType(), value);
            // use alias
            else
                return i.AliasName;
        }
    }
}
