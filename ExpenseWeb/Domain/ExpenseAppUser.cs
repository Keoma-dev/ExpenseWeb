﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWeb.Domain
{
    public class ExpenseAppUser : IdentityUser
    {
        public string Geslacht { get; set; }
    }
}
