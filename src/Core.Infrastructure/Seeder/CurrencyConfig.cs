using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Seeder
{
    public class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            var currencies = new List<Currency>()
            {
                new Currency() {
                    Code = "ALL",
                    UnicodeDecimal = "76, 101, 107",
                    UnicodeHex = "4c, 65, 6b",
                    Text = "Albania Lek"
                },
                new Currency() {
                    Code = "AFN",
                    UnicodeDecimal = "1547",
                    UnicodeHex = "60b",
                    Text = "Afghanistan Afghani"
                },
                new Currency() {
                    Code = "ARS",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Argentina Peso"
                },
                new Currency() {
                    Code = "AWG",
                    UnicodeDecimal = "402",
                    UnicodeHex = "192",
                    Text = "Aruba Guilder"
                },
                new Currency() {
                    Code = "AUD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Australia Dollar"
                },
                new Currency() {
                    Code = "AZN",
                    UnicodeDecimal = "1084, 1072, 1085",
                    UnicodeHex = "43c, 430, 43d",
                    Text = "Azerbaijan New Manat"
                },
                new Currency() {
                    Code = "BSD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Bahamas Dollar"
                },
                new Currency() {
                    Code = "BBD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Barbados Dollar"
                },
                new Currency() {
                    Code = "BYR",
                    UnicodeDecimal = "112, 46",
                    UnicodeHex = "70, 2e",
                    Text = "Belarus Ruble"
                },
                new Currency() {
                    Code = "BZD",
                    UnicodeDecimal = "66, 90, 36",
                    UnicodeHex = "42, 5a, 24",
                    Text = "Belize Dollar"
                },
                new Currency() {
                    Code = "BMD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Bermuda Dollar"
                },
                new Currency() {
                    Code = "BOB",
                    UnicodeDecimal = "36, 98",
                    UnicodeHex = "24, 62",
                    Text = "Bolivia Boliviano"
                },
                new Currency() {
                    Code = "BAM",
                    UnicodeDecimal = "75, 77",
                    UnicodeHex = "4b, 4d",
                    Text = "Bosnia and Herzegovina Convertible Marka"
                },
                new Currency() {
                    Code = "BWP",
                    UnicodeDecimal = "80",
                    UnicodeHex = "50",
                    Text = "Botswana Pula"
                },
                new Currency() {
                    Code = "BGN",
                    UnicodeDecimal = "1083, 1074",
                    UnicodeHex = "43b, 432",
                    Text = "Bulgaria Lev"
                },
                new Currency() {
                    Code = "BRL",
                    UnicodeDecimal = "82, 36",
                    UnicodeHex = "52, 24",
                    Text = "Brazil Real"
                },
                new Currency() {
                    Code = "BND",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Brunei Darussalam Dollar"
                },
                new Currency() {
                    Code = "KHR",
                    UnicodeDecimal = "6107",
                    UnicodeHex = "17db",
                    Text = "Cambodia Riel"
                },
                new Currency() {
                    Code = "CAD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Canada Dollar"
                },
                new Currency() {
                    Code = "KYD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Cayman Islands Dollar"
                },
                new Currency() {
                    Code = "CLP",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Chile Peso"
                },
                new Currency() {
                    Code = "CNY",
                    UnicodeDecimal = "165",
                    UnicodeHex = "a5",
                    Text = "China Yuan Renminbi"
                },
                new Currency() {
                    Code = "COP",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Colombia Peso"
                },
                new Currency() {
                    Code = "CRC",
                    UnicodeDecimal = "8353",
                    UnicodeHex = "20a1",
                    Text = "Costa Rica Colon"
                },
                new Currency() {
                    Code = "HRK",
                    UnicodeDecimal = "107, 110",
                    UnicodeHex = "6b, 6e",
                    Text = "Croatia Kuna"
                },
                new Currency() {
                    Code = "CUP",
                    UnicodeDecimal = "8369",
                    UnicodeHex = "20b1",
                    Text = "Cuba Peso"
                },
                new Currency() {
                    Code = "CZK",
                    UnicodeDecimal = "75, 269",
                    UnicodeHex = "4b, 10d",
                    Text = "Czech Republic Koruna"
                },
                new Currency() {
                    Code = "DKK",
                    UnicodeDecimal = "107, 114",
                    UnicodeHex = "6b, 72",
                    Text = "Denmark Krone"
                },
                new Currency() {
                    Code = "DOP",
                    UnicodeDecimal = "82, 68, 36",
                    UnicodeHex = "52, 44, 24",
                    Text = "Dominican Republic Peso"
                },
                new Currency() {
                    Code = "XCD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "East Caribbean Dollar"
                },
                new Currency() {
                    Code = "EGP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Egypt Pound"
                },
                new Currency() {
                    Code = "SVC",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "El Salvador Colon"
                },
                new Currency() {
                    Code = "EEK",
                    UnicodeDecimal = "107, 114",
                    UnicodeHex = "6b, 72",
                    Text = "Estonia Kroon"
                },
                new Currency() {
                    Code = "EUR",
                    UnicodeDecimal = "8364",
                    UnicodeHex = "20ac",
                    Text = "Euro Member Countries"
                },
                new Currency() {
                    Code = "FKP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Falkland Islands (Malvinas) Pound"
                },
                new Currency() {
                    Code = "FJD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Fiji Dollar"
                },
                new Currency() {
                    Code = "GHC",
                    UnicodeDecimal = "162",
                    UnicodeHex = "a2",
                    Text = "Ghana Cedis"
                },
                new Currency() {
                    Code = "GIP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Gibraltar Pound"
                },
                new Currency() {
                    Code = "GTQ",
                    UnicodeDecimal = "81",
                    UnicodeHex = "51",
                    Text = "Guatemala Quetzal"
                },
                new Currency() {
                    Code = "GGP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Guernsey Pound"
                },
                new Currency() {
                    Code = "GYD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Guyana Dollar"
                },
                new Currency() {
                    Code = "HNL",
                    UnicodeDecimal = "76",
                    UnicodeHex = "4c",
                    Text = "Honduras Lempira"
                },
                new Currency() {
                    Code = "HKD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Hong Kong Dollar"
                },
                new Currency() {
                    Code = "HUF",
                    UnicodeDecimal = "70, 116",
                    UnicodeHex = "46, 74",
                    Text = "Hungary Forint"
                },
                new Currency() {
                    Code = "ISK",
                    UnicodeDecimal = "107, 114",
                    UnicodeHex = "6b, 72",
                    Text = "Iceland Krona"
                },
                new Currency() {
                    Code = "INR",
                    UnicodeDecimal = "",
                    UnicodeHex = "",
                    Text = "India Rupee"
                },
                new Currency() {
                    Code = "IDR",
                    UnicodeDecimal = "82, 112",
                    UnicodeHex = "52, 70",
                    Text = "Indonesia Rupiah"
                },
                new Currency() {
                    Code = "IRR",
                    UnicodeDecimal = "65020",
                    UnicodeHex = "fdfc",
                    Text = "Iran Rial"
                },
                new Currency() {
                    Code = "IMP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Isle of Man Pound"
                },
                new Currency() {
                    Code = "ILS",
                    UnicodeDecimal = "8362",
                    UnicodeHex = "20aa",
                    Text = "Israel Shekel"
                },
                new Currency() {
                    Code = "JMD",
                    UnicodeDecimal = "74, 36",
                    UnicodeHex = "4a, 24",
                    Text = "Jamaica Dollar"
                },
                new Currency() {
                    Code = "JPY",
                    UnicodeDecimal = "165",
                    UnicodeHex = "a5",
                    Text = "Japan Yen"
                },
                new Currency() {
                    Code = "JEP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Jersey Pound"
                },
                new Currency() {
                    Code = "KZT",
                    UnicodeDecimal = "1083, 1074",
                    UnicodeHex = "43b, 432",
                    Text = "Kazakhstan Tenge"
                },
                new Currency() {
                    Code = "KPW",
                    UnicodeDecimal = "8361",
                    UnicodeHex = "20a9",
                    Text = "Korea (North) Won"
                },
                new Currency() {
                    Code = "KGS",
                    UnicodeDecimal = "1083, 1074",
                    UnicodeHex = "43b, 432",
                    Text = "Kyrgyzstan Som"
                },
                new Currency() {
                    Code = "LAK",
                    UnicodeDecimal = "8365",
                    UnicodeHex = "20ad",
                    Text = "Laos Kip"
                },
                new Currency() {
                    Code = "LVL",
                    UnicodeDecimal = "76, 115",
                    UnicodeHex = "4c, 73",
                    Text = "Latvia Lat"
                },
                new Currency() {
                    Code = "LBP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Lebanon Pound"
                },
                new Currency() {
                    Code = "LRD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Liberia Dollar"
                },
                new Currency() {
                    Code = "LTL",
                    UnicodeDecimal = "76, 116",
                    UnicodeHex = "4c, 74",
                    Text = "Lithuania Litas"
                },
                new Currency() {
                    Code = "MKD",
                    UnicodeDecimal = "1076, 1077, 1085",
                    UnicodeHex = "434, 435, 43d",
                    Text = "Macedonia Denar"
                },
                new Currency() {
                    Code = "MYR",
                    UnicodeDecimal = "82, 77",
                    UnicodeHex = "52, 4d",
                    Text = "Malaysia Ringgit"
                },
                new Currency() {
                    Code = "MUR",
                    UnicodeDecimal = "8360",
                    UnicodeHex = "20a8",
                    Text = "Mauritius Rupee"
                },
                new Currency() {
                    Code = "MXN",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Mexico Peso"
                },
                new Currency() {
                    Code = "MNT",
                    UnicodeDecimal = "8366",
                    UnicodeHex = "20ae",
                    Text = "Mongolia Tughrik"
                },
                new Currency() {
                    Code = "MZN",
                    UnicodeDecimal = "77, 84",
                    UnicodeHex = "4d, 54",
                    Text = "Mozambique Metical"
                },
                new Currency() {
                    Code = "NAD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Namibia Dollar"
                },
                new Currency() {
                    Code = "NPR",
                    UnicodeDecimal = "8360",
                    UnicodeHex = "20a8",
                    Text = "Nepal Rupee"
                },
                new Currency() {
                    Code = "ANG",
                    UnicodeDecimal = "402",
                    UnicodeHex = "192",
                    Text = "Netherlands Antilles Guilder"
                },
                new Currency() {
                    Code = "NZD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "New Zealand Dollar"
                },
                new Currency() {
                    Code = "NIO",
                    UnicodeDecimal = "67, 36",
                    UnicodeHex = "43, 24",
                    Text = "Nicaragua Cordoba"
                },
                new Currency() {
                    Code = "NGN",
                    UnicodeDecimal = "8358",
                    UnicodeHex = "20a6",
                    Text = "Nigeria Naira"
                },
                new Currency() {
                    Code = "NOK",
                    UnicodeDecimal = "107, 114",
                    UnicodeHex = "6b, 72",
                    Text = "Norway Krone"
                },
                new Currency() {
                    Code = "OMR",
                    UnicodeDecimal = "65020",
                    UnicodeHex = "fdfc",
                    Text = "Oman Rial"
                },
                new Currency() {
                    Code = "PKR",
                    UnicodeDecimal = "8360",
                    UnicodeHex = "20a8",
                    Text = "Pakistan Rupee"
                },
                new Currency() {
                    Code = "PAB",
                    UnicodeDecimal = "66, 47, 46",
                    UnicodeHex = "42, 2f, 2e",
                    Text = "Panama Balboa"
                },
                new Currency() {
                    Code = "PYG",
                    UnicodeDecimal = "71, 115",
                    UnicodeHex = "47, 73",
                    Text = "Paraguay Guarani"
                },
                new Currency() {
                    Code = "PEN",
                    UnicodeDecimal = "83, 47, 46",
                    UnicodeHex = "53, 2f, 2e",
                    Text = "Peru Nuevo Sol"
                },
                new Currency() {
                    Code = "PHP",
                    UnicodeDecimal = "8369",
                    UnicodeHex = "20b1",
                    Text = "Philippines Peso"
                },
                new Currency() {
                    Code = "PLN",
                    UnicodeDecimal = "122, 322",
                    UnicodeHex = "7a, 142",
                    Text = "Poland Zloty"
                },
                new Currency() {
                    Code = "QAR",
                    UnicodeDecimal = "65020",
                    UnicodeHex = "fdfc",
                    Text = "Qatar Riyal"
                },
                new Currency() {
                    Code = "RON",
                    UnicodeDecimal = "108, 101, 105",
                    UnicodeHex = "6c, 65, 69",
                    Text = "Romania New Leu"
                },
                new Currency() {
                    Code = "RUB",
                    UnicodeDecimal = "1088, 1091, 1073",
                    UnicodeHex = "440, 443, 431",
                    Text = "Russia Ruble"
                },
                new Currency() {
                    Code = "SHP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Saint Helena Pound"
                },
                new Currency() {
                    Code = "SAR",
                    UnicodeDecimal = "65020",
                    UnicodeHex = "fdfc",
                    Text = "Saudi Arabia Riyal"
                },
                new Currency() {
                    Code = "RSD",
                    UnicodeDecimal = "1044, 1080, 1085, 46",
                    UnicodeHex = "414, 438, 43d, 2e",
                    Text = "Serbia Dinar"
                },
                new Currency() {
                    Code = "SCR",
                    UnicodeDecimal = "8360",
                    UnicodeHex = "20a8",
                    Text = "Seychelles Rupee"
                },
                new Currency() {
                    Code = "SGD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Singapore Dollar"
                },
                new Currency() {
                    Code = "SBD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Solomon Islands Dollar"
                },
                new Currency() {
                    Code = "SOS",
                    UnicodeDecimal = "83",
                    UnicodeHex = "53",
                    Text = "Somalia Shilling"
                },
                new Currency() {
                    Code = "ZAR",
                    UnicodeDecimal = "82",
                    UnicodeHex = "52",
                    Text = "South Africa Rand"
                },
                new Currency() {
                    Code = "KRW",
                    UnicodeDecimal = "8361",
                    UnicodeHex = "20a9",
                    Text = "Korea (South) Won"
                },
                new Currency() {
                    Code = "LKR",
                    UnicodeDecimal = "8360",
                    UnicodeHex = "20a8",
                    Text = "Sri Lanka Rupee"
                },
                new Currency() {
                    Code = "SEK",
                    UnicodeDecimal = "107, 114",
                    UnicodeHex = "6b, 72",
                    Text = "Sweden Krona"
                },
                new Currency() {
                    Code = "CHF",
                    UnicodeDecimal = "67, 72, 70",
                    UnicodeHex = "43, 48, 46",
                    Text = "Switzerland Franc"
                },
                new Currency() {
                    Code = "SRD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Suriname Dollar"
                },
                new Currency() {
                    Code = "SYP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "Syria Pound"
                },
                new Currency() {
                    Code = "TWD",
                    UnicodeDecimal = "78, 84, 36",
                    UnicodeHex = "4e, 54, 24",
                    Text = "Taiwan New Dollar"
                },
                new Currency() {
                    Code = "THB",
                    UnicodeDecimal = "3647",
                    UnicodeHex = "e3f",
                    Text = "Thailand Baht"
                },
                new Currency() {
                    Code = "TTD",
                    UnicodeDecimal = "84, 84, 36",
                    UnicodeHex = "54, 54, 24",
                    Text = "Trinidad and Tobago Dollar"
                },
                new Currency() {
                    Code = "TRY",
                    UnicodeDecimal = "",
                    UnicodeHex = "",
                    Text = "Turkey Lira"
                },
                new Currency() {
                    Code = "TRL",
                    UnicodeDecimal = "8356",
                    UnicodeHex = "20a4",
                    Text = "Turkey Lira"
                },
                new Currency() {
                    Code = "TVD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "Tuvalu Dollar"
                },
                new Currency() {
                    Code = "UAH",
                    UnicodeDecimal = "8372",
                    UnicodeHex = "20b4",
                    Text = "Ukraine Hryvna"
                },
                new Currency() {
                    Code = "GBP",
                    UnicodeDecimal = "163",
                    UnicodeHex = "a3",
                    Text = "United Kingdom Pound"
                },
                new Currency() {
                    Code = "USD",
                    UnicodeDecimal = "36",
                    UnicodeHex = "24",
                    Text = "United States Dollar"
                },
                new Currency() {
                    Code = "UYU",
                    UnicodeDecimal = "36, 85",
                    UnicodeHex = "24, 55",
                    Text = "Uruguay Peso"
                },
                new Currency() {
                    Code = "UZS",
                    UnicodeDecimal = "1083, 1074",
                    UnicodeHex = "43b, 432",
                    Text = "Uzbekistan Som"
                },
                new Currency() {
                    Code = "VEF",
                    UnicodeDecimal = "66, 115",
                    UnicodeHex = "42, 73",
                    Text = "Venezuela Bolivar"
                },
                new Currency() {
                    Code = "VND",
                    UnicodeDecimal = "8363",
                    UnicodeHex = "20ab",
                    Text = "Viet Nam Dong"
                },
                new Currency() {
                    Code = "YER",
                    UnicodeDecimal = "65020",
                    UnicodeHex = "fdfc",
                    Text = "Yemen Rial"
                },
                new Currency() {
                    Code = "ZWD",
                    UnicodeDecimal = "90, 36",
                    UnicodeHex = "5a, 24",
                    Text = "Zimbabwe Dollar"
                }
            };

            builder.HasData(currencies);
        }
    }
}
