using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseWeb.Database;
using ExpenseWeb.Domain;
using ExpenseWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ExpenseWeb.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IExpenseDatabase _expenseDatabase;

        public StatisticsController(IExpenseDatabase expenseDatabase)
        {
            _expenseDatabase = expenseDatabase;
        }
        public IActionResult Statistics()
        {
            return View();
        }

        public IActionResult Highest()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                Expense expense = expenses.OrderByDescending(item => item.Bedrag).First();
                ExpenseStatisticsViewModel showExpense = new ExpenseStatisticsViewModel()
                {
                    Omschrijving = expense.Omschrijving,
                    Datum = expense.Datum,
                    Bedrag = expense.Bedrag
                };
                return View(showExpense);
            }
            else
            {
                return View();
            }
            
        }
        public IActionResult Lowest()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                Expense expense = expenses.OrderByDescending(item => item.Bedrag).Last();
            ExpenseStatisticsViewModel showExpense = new ExpenseStatisticsViewModel()
            {
                Omschrijving = expense.Omschrijving,
                Datum = expense.Datum,
                Bedrag = expense.Bedrag
            };
            return View(showExpense);
            }
            else
            {
                return View();
            }

        }
        public IActionResult HighestDay()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                var groupedByDate = expenses.GroupBy(expense => expense.Datum.Date);
            ExpenseStatisticsViewModel newExpense = new ExpenseStatisticsViewModel() { Bedrag = 0 };

            foreach (var expense in groupedByDate)
            {                
                decimal totalExpense = 0;
                foreach (var bedrag in expense)
                {
                    totalExpense += bedrag.Bedrag;
                }
                if (totalExpense > newExpense.Bedrag)
                {
                    newExpense.Bedrag = totalExpense;
                    newExpense.Datum = expense.Key;
                }
            }

            return View(newExpense);
            }
            else
            {
                return View();
            }
        }
        public IActionResult OverViewPerMonth()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                var groupedByDate = expenses.GroupBy(expense => expense.Datum.Month);
            List<ExpenseListViewModel> expensesPerMonth = new List<ExpenseListViewModel>();

            foreach (var expense in groupedByDate)
            {
                decimal totalExpense = 0;
                DateTime date = DateTime.Today;
                foreach (var bedrag in expense)
                {
                    totalExpense += bedrag.Bedrag;
                    date = bedrag.Datum;
                }

                expensesPerMonth.Add(new ExpenseListViewModel { Bedrag = totalExpense, Datum = date });
            }           

            return View(expensesPerMonth);
            }
            else
            {
                return View();
            }
        }

        public IActionResult HighestCategory()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                var groupedByCategory = expenses.GroupBy(expense => expense.Categorie);
                ExpenseStatisticsViewModel newExpense = new ExpenseStatisticsViewModel() { Bedrag = 0 };

                foreach (var expense in groupedByCategory)
                {
                    decimal totalExpense = 0;
                    foreach (var bedrag in expense)
                    {
                        totalExpense += bedrag.Bedrag;
                    }
                    if (totalExpense > newExpense.Bedrag)
                    {
                        newExpense.Bedrag = totalExpense;
                        newExpense.Categorie = expense.Key;
                    }
                }

                return View(newExpense);
            }
            else
            {
                return View();
            }
        }
        public IActionResult CheapestCategory()
        {
            IEnumerable<Expense> expenses = _expenseDatabase.GetExpenses();
            if (expenses.Count() > 0)
            {
                var groupedByCategory = expenses.GroupBy(expense => expense.Categorie);
                ExpenseStatisticsViewModel newExpense = new ExpenseStatisticsViewModel() { Bedrag = 0 };
                newExpense.Bedrag = 0;

                foreach (var expense in groupedByCategory)
                {
                    decimal totalExpense = 0;
                    foreach (var bedrag in expense)
                    {
                        totalExpense += bedrag.Bedrag;
                    }                   
                    if (totalExpense < newExpense.Bedrag || newExpense.Bedrag == 0)
                    {
                        newExpense.Bedrag = totalExpense;
                        newExpense.Categorie = expense.Key;
                    }
                }

                return View(newExpense);
            }
            else
            {
                return View();
            }
        }
    }
}
