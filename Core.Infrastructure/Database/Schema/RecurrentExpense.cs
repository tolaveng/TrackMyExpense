using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database.Schema
{
    public class RecurrentExpense : Expense
    {
        public bool IsCompleted { get; set; }
        public bool IsRecurrent { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}
