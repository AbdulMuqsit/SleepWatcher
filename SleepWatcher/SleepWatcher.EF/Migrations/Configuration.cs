using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                patient.Steps = new List<Step>();
                Step step = new Step();
                DateTime alarmTime = rand.Next(0, 5) == 1 ? DateTime.Now - TimeSpan.FromDays(30) : DateTime.Now + TimeSpan.FromDays(30);
                for (int j = 0; j < totalSteps; j++)
                {

                    step = new Step()
                    {
                        DateAdded = DateTime.Now,
                        AlarmTime = alarmTime,
                        StepName = (StepName)j,
                        IsCompleted = true,
                        IsCancled = false,

                    };
                    int totalNotes = rand.Next(10);
                    step.Notes = new Collection<Note>();
                    for (int k = 0; k < totalNotes; k++)
                    {
                        step.Notes.Add(new Note() { Title = Ipsum.GetPhrase(rand.Next(10)), Date = DateTime.Now, Text = Ipsum.GetPhrase(rand.Next(100)) });
                    }
                    patient.Steps.Add(step);
                    if (canceled)
                    {
                        step.IsCompleted = false;
                        step.IsCancled = true;
                        patient.CurrentStep = new CurrentStep() { AlarmTime = step.AlarmTime, IsCancled = step.IsCancled, IsCompleted = step.IsCompleted, StepName = step.StepName };
                        break;
                    }
                    if (j == totalSteps - 1)
                    {
                        step.IsCompleted = false;
                        patient.CurrentStep = new CurrentStep() { AlarmTime = step.AlarmTime, IsCancled = step.IsCancled, IsCompleted = step.IsCompleted, StepName = step.StepName };
                    }

                }

                patients.Add(patient);

            }


            context.Patients.AddRange(patients);
            base.Seed(context);

        }


    }
}
