using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tests
{
    public static class ConstantMock
    {
        public static readonly Guid UserId = Guid.Parse("5D7762D2-EC5B-46E1-AAE9-7206591E3CC2");
        public static readonly Guid IconId = Guid.Parse("CFC2D349-BBAA-4228-8B0C-8CF91714070B");
        public static readonly Guid BudgetJarId1 = Guid.Parse("BCA42767-831C-43A4-A3AC-9ECBC74A223F");
        public static readonly Guid BudgetJarId2 = Guid.Parse("C7F0319A-719F-4A0D-872B-96E4FD2CC6F2");
        public static readonly Guid ExpenseId = Guid.Parse("4B3F4C32-3DDD-4A67-BC24-B725B8883274");
        public static readonly Guid IncomeId = Guid.Parse("C8808AAB-4123-4667-9632-D0392C96F2ED");
    }
}
