﻿using System;

namespace ExpenseWeb.Models
{
    public class ExpenseListViewModel
    {
        public int Id { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public decimal Bedrag { get; set; }
    }
        
}
