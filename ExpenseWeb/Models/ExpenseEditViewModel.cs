using System;

namespace ExpenseWeb.Models
{
    public class ExpenseEditViewModel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public decimal Bedrag { get; set; }
    }
        
}
