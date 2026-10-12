using Microsoft.AspNetCore.Authorization;

namespace ClassLibrary.Authorization;

public class HasAttribute : AuthorizeAttribute
{
    public HasAttribute(object permission)
    {
        if (permission is not Enum enumValue)
            throw new ArgumentException("Permission must be an enum");

        var typeName = enumValue.GetType().Name;

        var resource = typeName.EndsWith("Permission")
            ? typeName.Replace("Permission", "").ToLower()
            : typeName.ToLower();

        var action = enumValue.ToString().ToLower();

        Policy = $"{resource}.{action}";
    }
}