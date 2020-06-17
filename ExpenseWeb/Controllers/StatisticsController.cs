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
        private readonly ExpenseDbContext _expenseDbContext;

        public StatisticsController(ExpenseDbContext expenseDbContext)
        {
            _expenseDbContext = expenseDbContext;
        }
        public IActionResult Statistics()
        {
            IEnumerable<Expense> expenses = _expenseDbContext.Expenses.ToList();

            if (expenses.Count() > 0)
            {
                ExpenseStatisticsViewModel newStatistics = new ExpenseStatisticsViewModel();
                newStatistics.HoogsteBedrag = Highest(expenses);
                newStatistics.LaagsteBedrag = Lowest(expenses);
                newStatistics.DuursteDag = HighestDay(expenses);
                newStatistics.ExpensesPerMonth = OverViewPerMonth(expenses);
                newStatistics.DuursteCategorie = HighestCategory(expenses);
                newStatistics.GoedkoopsteCategorie = CheapestCategory(expenses);
                return View(newStatistics);
            }
            else
            {
                return View();
            }
        }

        public Expense Highest(IEnumerable<Expense> expenses)
        {                        
                Expense expense = expenses.OrderByDescending(item => item.Bedrag).First();
                Expense highestExpense = new Expense() { Bedrag = expense.Bedrag, Omschrijving = expense.Omschrijving, Categorie = expense.Categorie, Datum = expense.Datum };             

                return highestExpense;                    
            
        }
        public Expense Lowest(IEnumerable<Expense> expenses)
        {           
            Expense expense = expenses.OrderByDescending(item => item.Bedrag).Last();
            Expense lowestExpense = new Expense() { Bedrag = expense.Bedrag, Omschrijving = expense.Omschrijving, Categorie = expense.Categorie, Datum = expense.Datum };

            return lowestExpense;

        }
        public Expense HighestDay(IEnumerable<Expense> expenses)
        {           
            var groupedByDate = expenses.GroupBy(expense => expense.Datum.Date);
            Expense highestDay = new Expense() { Bedrag = 0 };

            foreach (var expense in groupedByDate)
            {                
                decimal totalExpense = 0;
                foreach (var bedrag in expense)
                {
                    totalExpense += bedrag.Bedrag;
                }
                if (totalExpense > highestDay.Bedrag)
                {
                    highestDay.Bedrag = totalExpense;
                    highestDay.Datum = expense.Key;
                }
            }

            return highestDay;
            
        }
        public List<Expense> OverViewPerMonth(IEnumerable<Expense> expenses)
        {           
            var groupedByDate = expenses.GroupBy(expense => expense.Datum.Month);
            List<Expense> expensesPerMonth = new List<Expense>();

            foreach (var expense in groupedByDate)
            {
                decimal totalExpense = 0;
                DateTime date = DateTime.Today;
                foreach (var bedrag in expense)
                {
                    totalExpense += bedrag.Bedrag;
                    date = bedrag.Datum;
                }

                expensesPerMonth.Add(new Expense { Bedrag = totalExpense, Datum = date });
            }

            return expensesPerMonth;
        }

        public Expense HighestCategory(IEnumerable<Expense> expenses)
        {
            
                var groupedByCategory = expenses.GroupBy(expense => expense.Categorie);
                Expense newExpense = new Expense { Bedrag = 0 };

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

                return newExpense;                       
        }
        public Expense CheapestCategory(IEnumerable<Expense> expenses)
        {
           
                var groupedByCategory = expenses.GroupBy(expense => expense.Categorie);
                Expense newExpense = new Expense { Bedrag = 0 };
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

                return newExpense;
            
        }
    }
}
