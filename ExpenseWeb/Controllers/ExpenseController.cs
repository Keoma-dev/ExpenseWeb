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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseWeb.Controllers
{
    public class ExpenseController : Controller
    {
        
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ExpenseDbContext _expenseDbContext;

        public ExpenseController(ExpenseDbContext expenseDbContext, IWebHostEnvironment hostEnvironment)
        {
            _expenseDbContext = expenseDbContext;
            _hostEnvironment = hostEnvironment;
            
        }

        public async Task<IActionResult> Create()
        {
            ExpenseCreateViewModel vm = new ExpenseCreateViewModel();

            var paidStatuses = await _expenseDbContext.PaidStatuses.ToListAsync();

            foreach (PaidStatus paidStatus in paidStatuses)
            {
                vm.PaidStatuses.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                {
                    Value = paidStatus.Id.ToString(),
                    Text = paidStatus.Name
                });

            }
            return View(vm);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(ExpenseCreateViewModel expense)
        {
            if (!TryValidateModel(expense))
            {
                var paidStatuses = await _expenseDbContext.PaidStatuses.ToListAsync();

                foreach (PaidStatus paidStatus in paidStatuses)
                {
                    expense.PaidStatuses.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
                    {
                        Value = paidStatus.Id.ToString(),
                        Text = paidStatus.Name
                    });

                }
                return View(expense);
            }

            Expense newExpense = new Expense()
            {
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag,
                Categorie = expense.Categorie,
                PaidStatusId = expense.SelectedPaidStatus

            };

            if (expense.Photo != null )
            {
                string uniqueFileName = UploadExpensePhoto(expense.Photo);
                newExpense.PhotoUrl = "/photos/" + uniqueFileName;
            }

            _expenseDbContext.Expenses.Add(newExpense);
            await _expenseDbContext.SaveChangesAsync();
            

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Expense> expensesFromDb = await _expenseDbContext.Expenses.Include(x=>x.PaidStatus).ToListAsync();
            List<ExpenseListViewModel> expenses = new List<ExpenseListViewModel>();
           

            foreach (var expense in expensesFromDb)
            {
                expenses.Add(new ExpenseListViewModel 
                    { Id = expense.Id, 
                      Omschrijving = expense.Omschrijving, 
                      Datum = expense.Datum, Bedrag = expense.Bedrag, 
                      Categorie = expense.Categorie, 
                      PhotoUrl = expense.PhotoUrl, 
                      PaidStatus = expense.PaidStatus.Name
                    });
            }
            return View(expenses);
        }        
    
        public async Task<IActionResult> Delete(int id)
        {
            Expense expenseToDelete = await _expenseDbContext.Expenses.FindAsync(id);
            _expenseDbContext.Expenses.Remove(expenseToDelete);
            await _expenseDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
            
        }
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expenseDbContext.Expenses.FindAsync(id);
            var paidStatuses = await _expenseDbContext.PaidStatuses.ToListAsync();
          
            ExpenseEditViewModel showExpense = new ExpenseEditViewModel()
            {                
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag,
                Categorie = expense.Categorie,
                PhotoUrl = expense.PhotoUrl,
            };

            foreach (PaidStatus paidStatus in paidStatuses)
            {
                showExpense.PaidStatuses.Add(new SelectListItem()
                {
                    Value = paidStatus.Id.ToString(),
                    Text = paidStatus.Name
                });
            }
            return View(showExpense);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseEditViewModel editedExpense)
        {
            var expense = new Expense()
            {
                Id = id,
                Omschrijving = editedExpense.Omschrijving,
                Datum = editedExpense.Datum,
                Bedrag = editedExpense.Bedrag,
                Categorie = editedExpense.Categorie,
                PaidStatusId = editedExpense.SelectedPaidStatus
            };

            string uniqueFileName = UploadExpensePhoto(editedExpense.Photo);
            expense.PhotoUrl = "/photos/" + uniqueFileName;

            _expenseDbContext.Update(expense);

            await _expenseDbContext.SaveChangesAsync();
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
