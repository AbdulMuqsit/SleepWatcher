using System.Collections.Generic;
using System.Diagnostics;
using SleepWatcher.Entites;

namespace SleepWatcher.EF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SleepWatcher.EF.SleepWatcherDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            
        }

        protected override void Seed(SleepWatcherDbContext context)
        {
            Random rand = new Random();

            IList<Patient> patients = new List<Patient>();
            
            for (int i = 0; i < 200; i++)
            {
                var patient = new Patient()
                {
                    FirstName = "Bill" + i,
                    LastName = "Peters"
                };
                var totalSteps = rand.Next(1, 7);
                bool canceled = rand.Next(0, 9) == 0;
                patient.Steps=new List<Step>();
                for (int j = 0; j < totalSteps; j++)
                {
                    
                    Step step = new Step()
                    {
                        DateAdded = DateTime.Now,
                        AlarmTime = DateTime.Now,
                        StepName = (StepName)j,
                        IsCompleted = true,
                        IsCancled = false,
                        Notes = new List<Note>() { new Note() { Text = "abc" } }
                    };
                    patient.Steps.Add(step);
                    if (canceled)
                    {
                        step.IsCompleted = false;
                        step.IsCancled = true;
                        break;
                    }
                    if (j == totalSteps - 1)
                    {
                        step.IsCompleted = false;
                    }

                }
               patients.Add(patient);

            }


            context.Patients.AddRange(patients);
            base.Seed(context);
            var p = context.Patients.ToList();
            Debug.WriteLine("Total Patients:"+p.Count);
        }

      
    }
}
