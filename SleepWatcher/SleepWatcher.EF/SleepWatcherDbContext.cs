using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleepWatcher.Entites;

namespace SleepWatcher.EF
{
    public class SleepWatcherDbContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Step> Steps { get; set; }

        public SleepWatcherDbContext()
            : base("SleepWatcherDb")
        {
            
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasMany(e => e.Steps)
                .WithRequired(e => e.Patient)
                .HasForeignKey(e => e.PatientId);
            modelBuilder.Entity<Step>()
                .HasMany(e => e.Notes)
                .WithRequired(e => e.Step)
                .HasForeignKey(e => e.StepId);
            
        }
    }
}
