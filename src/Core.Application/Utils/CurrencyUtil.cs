using Core.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Utils
{
    public static class CurrencyUtil
    {
        public static string Formatted(decimal number, string cultureInfoName)
        {
            if(string.IsNullOrEmpty(cultureInfoName))
            {
                cultureInfoName = DefaultConstants.DefaultCultureInfo;
            }

            var numberFormat = new CultureInfo(cultureInfoName, false).NumberFormat;
            return string.Format(numberFormat, "{0:c2}", number);
        }

        public static string CurrencySymbol(string cultureInfoName)
        {
            if (string.IsNullOrEmpty(cultureInfoName))
            {
                cultureInfoName = DefaultConstants.DefaultCultureInfo;
            }

            try
            {
                // Not all culture has valid region info
                var regionInfo = new RegionInfo(new CultureInfo(cultureInfoName).LCID);
                return regionInfo.CurrencySymbol;
            }
            catch (Exception)
            {
                var regionInfo = new RegionInfo(new CultureInfo(DefaultConstants.DefaultCultureInfo).LCID);
                return regionInfo.CurrencySymbol;
            }
        }
    }
}
