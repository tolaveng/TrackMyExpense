using Core.Domain.Entities;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Constants
{
    public class DefaultConstants
    {
        public const string PageTitle = "Track My Expense";
        public const string DefaultTimeZone = "Australia/Sydney";
        public const string DefaultCultureInfo = "en-AU";
        public const string DefaultDateTimeFormat = "dd/MM/yyyy";

        public static Icon DefaultIcon = new Icon()
        {
            Id = Guid.Parse("6B7C5AD3-82C5-4AFC-AD66-A2A895A4BF7B"),
            Name = "Others",
            Path = "/assets/icons/others.png",
            IconType = IconType.Asset,
        };
    }
}
