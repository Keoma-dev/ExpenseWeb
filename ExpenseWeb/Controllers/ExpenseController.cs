using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseWeb.Database;
using ExpenseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseWeb.Domain;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace ExpenseWeb.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly IExpenseDatabase _expenseDatabase;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ExpenseController(IExpenseDatabase expenseDatabase, IWebHostEnvironment hostEnvironment)
        {
            _expenseDatabase = expenseDatabase;
            _hostEnvironment = hostEnvironment;
            
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

            if (expense.Photo != null )
            {
                string uniqueFileName = UploadExpensePhoto(expense.Photo);
                newExpense.PhotoUrl = "/photos/" + uniqueFileName;
            }

            _expenseDatabase.Insert(newExpense);
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            IEnumerable<Expense> expensesFromDb = _expenseDatabase.GetExpenses();
            List<ExpenseListViewModel> expenses = new List<ExpenseListViewModel>();

            foreach (var expense in expensesFromDb)
            {
                expenses.Add(new ExpenseListViewModel { Id = expense.Id, Omschrijving = expense.Omschrijving, Datum = expense.Datum, Bedrag = expense.Bedrag, Categorie = expense.Categorie, PhotoUrl = expense.PhotoUrl });
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
                Categorie = expense.Categorie,
                PhotoUrl = expense.PhotoUrl

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

            string uniqueFileName = UploadExpensePhoto(editedExpense.Photo);
            expense.PhotoUrl = "/photos/" + uniqueFileName;

            _expenseDatabase.Update(id, expense);
            return RedirectToAction("Index");
        }   
        public string UploadExpensePhoto(IFormFile photo)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
            string pathName = Path.Combine(_hostEnvironment.WebRootPath, "photos");
            string fileNameWithPath = Path.Combine(pathName, uniqueFileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                photo.CopyTo(stream);
            }

            return uniqueFileName;
        }

        
    }
}
