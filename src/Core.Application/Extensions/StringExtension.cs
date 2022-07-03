using System;
using System.Text.RegularExpressions;
using System.Web;

namespace Core.Application.Extensions
{
    public static class StringExtension
    {
        public static string SanitiseFileName (this string fileName)
        {
            string invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);
            fileName = Regex.Replace(fileName, invalidRegStr, "_").Replace(" ", "_").Trim();

            // if file too long, cut the last part
            if (fileName.Length > 64)
            {
                var ext = Path.GetExtension(fileName);
                var name = Path.GetFileNameWithoutExtension(fileName).Substring(0, 60);
                fileName = $"{name}{ext}";
            }

            return fileName;
        }

        public static string GetHtmlText (this string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue)) return "";

            return Regex.Replace(
                HttpUtility.HtmlEncode(stringValue), "\r?\n|\r", "<br />");
        }
    }
}
