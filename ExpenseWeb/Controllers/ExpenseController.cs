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
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(ExpenseCreateViewModel expense)
        {
            Expense newExpense = new Expense()
            {
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag,
                Categorie = expense.Categorie
                
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
                expenses.Add(new ExpenseListViewModel { Id = expense.Id, Omschrijving = expense.Omschrijving, Datum = expense.Datum, Bedrag = expense.Bedrag, Categorie = expense.Categorie });
            }
            return View(expenses);
        }        
    
        public IActionResult Delete(int id)
        {
            _expenseDatabase.Delete(id);
            return RedirectToAction("Index");
            
        }
        public IActionResult Edit(int id)
        {
            var expense = _expenseDatabase.GetExpense(id);
            ExpenseEditViewModel showExpense = new ExpenseEditViewModel()
            {                
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag,
                Categorie = expense.Categorie
            };
            return View(showExpense);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, ExpenseEditViewModel editedExpense)
        {
            var expense = new Expense()
            {
                Id = id,
                Omschrijving = editedExpense.Omschrijving,
                Datum = editedExpense.Datum,
                Bedrag = editedExpense.Bedrag,
                Categorie = editedExpense.Categorie                
            };

            _expenseDatabase.Update(id, expense);
            return RedirectToAction("Index");
        }       

        
    }
}
