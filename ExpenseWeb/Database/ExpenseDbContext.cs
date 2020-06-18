using ExpenseWeb.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseWeb.Database
{
    
    public class ExpenseDbContext : DbContext
    {
        public ExpenseDbContext(DbContextOptions<ExpenseDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PaidStatus>().HasData(
                new PaidStatus()
                {
                    Id = 1,
                    Name = "Nog niet betaald"
                },
                new PaidStatus()
                {
                    Id = 2,
                    Name = "Al betaald"
                });

            modelBuilder.Entity<PersonExpense>()
                .HasKey(pe => new { pe.ExpenseId, pe.PersonId });

            modelBuilder.Entity<PersonExpense>()
                .HasOne(pe => pe.Expense)
                .WithMany(p => p.PersonExpenses)
                .HasForeignKey(pe => pe.ExpenseId);

            modelBuilder.Entity<PersonExpense>()
                .HasOne(pe => pe.Person)
                .WithMany(e => e.PersonExpenses)
                .HasForeignKey(pe => pe.PersonId);

            modelBuilder.Entity<Person>()
                .HasData(
                    new Person() { Id = 1, Name = "Marc" },
                    new Person() { Id = 2, Name = "An" },
                    new Person() { Id = 3, Name = "Erik" },
                    new Person() { Id = 4, Name = "Ria" });
            
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonExpense> PersonExpenses { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<PaidStatus> PaidStatuses { get; set; }
    }
}
