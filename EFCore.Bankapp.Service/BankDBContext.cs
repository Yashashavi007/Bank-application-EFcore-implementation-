using EFCore.Bankapp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCore.Bankapp.Service
{
    public class BankDBContext : DbContext
    {
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conString = "server=localhost;user id=root;password=J@gu@r007;persistsecurityinfo=True;database=EFCoreBank";
            optionsBuilder.UseMySQL(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bank>().HasKey(e => new { e.ID, e.IFSCCode });

            modelBuilder.Entity<Customer>().HasAlternateKey(c => c.AccountNumber);
            //modelBuilder.Entity<Customer>().HasOne(b => b.Bank).WithMany(C => C.Customers).IsRequired();

            //modelBuilder.Entity<Transaction>().HasOne(c => c.Customer).WithMany(t => t.Transactions).IsRequired();

        }


    }

}
