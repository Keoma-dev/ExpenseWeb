using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWeb.Domain
{
    public class Expense
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public decimal Bedrag { get; set; }
        public string Categorie { get; set; }
        public string PhotoUrl { get; set; }
        public PaidStatus PaidStatus { get; set; }
        public int PaidStatusId { get; set; }

        public ICollection<PersonExpense> PersonExpenses { get; set; }
    }
}
