using System;
using System.Linq;
namespace SharpChatwork
{
    public class EnumAliasAttribute : Attribute
    {
        public string AliasName { get; set; }
        public EnumAliasAttribute(string aliasName)
        {
            this.AliasName = aliasName;
        }
    }
    public static class EnumAliasExtension
    {
        public static string ToAliasOrDefault(this Enum value)
        {
            var i = value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(EnumAliasAttribute), false)
                .Cast<EnumAliasAttribute>()
                .FirstOrDefault();
            if (i == null)
            {
                return Enum.GetName(value.GetType(), value);
            }
            else
            {
                return i.AliasName;
            }
        }
    }
}
