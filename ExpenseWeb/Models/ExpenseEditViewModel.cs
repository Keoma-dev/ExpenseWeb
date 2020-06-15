using Microsoft.AspNetCore.Http;
using System;

namespace ExpenseWeb.Models
{
    public class ExpenseEditViewModel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public decimal Bedrag { get; set; }
        public string Categorie { get; set; }
        public string PhotoUrl { get; set; }

        public IFormFile Photo { get; set; }
    }
        
}
