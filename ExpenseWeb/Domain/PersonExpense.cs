using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWeb.Domain
{
    public class PersonExpense
    {
        public int ExpenseId { get; set; }
        public Expense Expense { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
