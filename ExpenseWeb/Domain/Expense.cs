﻿using System;
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
    }
}