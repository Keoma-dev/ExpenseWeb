using ExpenseWeb.Domain;
using System;
using System.Collections.Generic;

namespace ExpenseWeb.Models
{
    public class ExpenseStatisticsViewModel
    {
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }

        public decimal Bedrag { get; set; }
        public string Categorie { get; set; }

        public Expense HoogsteBedrag { get; set; }
        public Expense LaagsteBedrag { get; set; }
        public Expense DuursteDag { get; set; }
        public List<Expense> ExpensesPerMonth { get; set; }
        public Expense DuursteCategorie { get; set; }
        public Expense GoedkoopsteCategorie { get; set; }
    }
        
}
