using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Extensions
{
    public static class EnumExtension
    {
        public static string GetDisplayName(this Enum enumValue) {
            var displayName = enumValue.GetType()
                .GetMember(enumValue.ToString()).FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?.GetName();

            if (!string.IsNullOrEmpty(displayName)) return displayName;
            return enumValue.ToString();
        }
    }
}
