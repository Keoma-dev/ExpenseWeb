using System;

namespace ExpenseWeb.Models
{
    public class ExpenseCreateViewModel
    {
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }        
        public decimal Bedrag { get; set; }
    }
        
}
