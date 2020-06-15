using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseWeb.Models
{
    public class ExpenseCreateViewModel
    {
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }

        public decimal Bedrag { get; set; }
        public string Categorie { get; set; }

        public IFormFile Photo { get; set; }
    }

}
 