using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseWeb.Database;
using ExpenseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseWeb.Domain;

namespace ExpenseWeb.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseDatabase _expenseDatabase;

        public ExpenseController(IExpenseDatabase expenseDatabase)
        {
            _expenseDatabase = expenseDatabase;
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ExpenseCreateViewModel expense)
        {
            Expense newExpense = new Expense()
            {
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag
            };

            _expenseDatabase.Insert(newExpense);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> expensesFromDb = _expenseDatabase.GetExpenses();
            List<ExpenseListViewModel> expenses = new List<ExpenseListViewModel>();

            foreach (var expense in expensesFromDb)
            {
                expenses.Add(new ExpenseListViewModel { Id = expense.Id, Omschrijving = expense.Omschrijving, Datum = expense.Datum, Bedrag = expense.Bedrag });
            }
            return View(expenses);
        }
    }
}
