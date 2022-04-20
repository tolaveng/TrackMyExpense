using Core.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests
{
    public class DateTimeUtilTest
    {
        private string _testTimeZoneId = "Asia/Phnom_Penh";
        private DateTime _testDateTimeNow = new DateTime(2022, 4, 1, 10, 0, 0, DateTimeKind.Unspecified);
        private DateTime _testDateTimeUtc = new DateTime(2022, 4, 1, 3, 0, 0, DateTimeKind.Utc);

        [Fact]
        public void Convert_To_Utc_Test()
        {
            var utcTest = DateTimeUtil.ToUtcDateTime(_testDateTimeNow, _testTimeZoneId);
            Assert.Equal(utcTest, _testDateTimeUtc);
        }

        [Fact]
        public void Convert_To_TimeZone_Test()
        {
            var timeZoneDateTime = DateTimeUtil.ToTimeZoneDateTime(_testDateTimeUtc, _testTimeZoneId);
            Assert.Equal(timeZoneDateTime, _testDateTimeNow);
        }
    }
}
