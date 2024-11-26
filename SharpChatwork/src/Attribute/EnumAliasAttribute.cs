using System;
using System.Linq;

namespace SharpChatwork;

[AttributeUsage(AttributeTargets.Field)]
internal sealed class EnumAliasAttribute(string aliasName) : Attribute
{
    public string aliasName { get; set; } = aliasName;
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
        if(i == null)
            return Enum.GetName(value.GetType(), value);
        // use alias
        return i.aliasName;
    }
}
