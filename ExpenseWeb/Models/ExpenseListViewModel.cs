using System;
using System.Collections.Generic;

namespace ExpenseWeb.Models
{
    public class ExpenseListViewModel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public decimal Bedrag { get; set; }
        public string Categorie { get; set; }
        public string PhotoUrl { get; set; }
        public string PaidStatus { get; set; }
        public IEnumerable<string> Persons { get; set; }
    }
        
}
