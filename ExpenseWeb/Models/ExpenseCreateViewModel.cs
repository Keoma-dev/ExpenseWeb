using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public List<SelectListItem> PaidStatuses { get; set; } = new List<SelectListItem>();
        public int SelectedPaidStatus { get; set; }

        public List<SelectListItem> Persons { get; set; } = new List<SelectListItem>();
        public int[] SelectedPersons { get; set; }
    }

}
 