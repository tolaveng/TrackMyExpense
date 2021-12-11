using System;

namespace Core.Domain.Entities
{
    public class RecurrentExpense : Expense
    {
        public bool IsCompleted { get; set; }
        public bool IsRecurrent { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
