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

            var persons = await _expenseDbContext.Persons.ToListAsync();
            vm.Persons = persons.Select(person => new SelectListItem() { Value = person.Id.ToString(), Text = person.Name }).ToList
                ();
           
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
                PaidStatusId = expense.SelectedPaidStatus,
                PersonExpenses = expense.SelectedPersons.Select(person => new PersonExpense() { PersonId = person }).ToList()

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
            IEnumerable<Expense> expensesFromDb = await _expenseDbContext.Expenses.Include(x=>x.PaidStatus).Include(x=>x.PersonExpenses).ThenInclude(personExpense=>personExpense.Person).ToListAsync();
            List<ExpenseListViewModel> expenses = new List<ExpenseListViewModel>();
           

            foreach (var expense in expensesFromDb)
            {
                expenses.Add(new ExpenseListViewModel 
                    { Id = expense.Id, 
                      Omschrijving = expense.Omschrijving, 
                      Datum = expense.Datum, Bedrag = expense.Bedrag, 
                      Categorie = expense.Categorie, 
                      PhotoUrl = expense.PhotoUrl, 
                      PaidStatus = expense.PaidStatus.Name,
                      Persons = expense.PersonExpenses.Select(personExpense => personExpense.Person.Name)
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

            var persons = await _expenseDbContext.Persons.ToListAsync();
            showExpense.Persons = persons.Select(person => new SelectListItem() { Value = person.Id.ToString(), Text = person.Name }).ToList();

            return View(showExpense);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int id, ExpenseEditViewModel editedExpense)
        {

            Expense domainExpense = await _expenseDbContext.Expenses.Include(e => e.PersonExpenses).FirstOrDefaultAsync(e => e.Id == id);

            _expenseDbContext.PersonExpenses.RemoveRange(domainExpense.PersonExpenses);


            domainExpense.Omschrijving = editedExpense.Omschrijving;
            domainExpense.Datum = editedExpense.Datum;
            domainExpense.Bedrag = editedExpense.Bedrag;
            domainExpense.Categorie = editedExpense.Categorie;
            domainExpense.PaidStatusId = editedExpense.SelectedPaidStatus;
            domainExpense.PersonExpenses = editedExpense.SelectedPersons.Select(person => new PersonExpense() { PersonId = person }).ToList();
            

            if (editedExpense.Photo != null)
            {
                string uniqueFileName = UploadExpensePhoto(editedExpense.Photo);
                domainExpense.PhotoUrl = "/photos/" + uniqueFileName;
            }

            _expenseDbContext.Update(domainExpense);

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
