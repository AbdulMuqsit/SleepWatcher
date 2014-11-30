using System.Collections.Generic;
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
          //  Seed(new SleepWatcherDbContext());
        }

        protected override void Seed(SleepWatcherDbContext context)
        {
            Random rand = new Random();

            IList<Patient> patients = new List<Patient>();
            for (int i = 0; i < 200; i++)
            {

                switch (rand.Next(0, 3))
                {
                    case 0:
                        patients.Add(new Patient()
                        {
                            Steps = new List<Step>()
                            {
                                new Step()
                                {

                                    DateAdded = DateTime.Now,
                                    AlarmTime = DateTime.Now,
                                    StepName = StepName.FollowUp,
                                    IsCompleted = true,
                                    Notes = new List<Note>() {new Note() { Text = "abc"} }
                                }
                            },
                            FirstName = "Bill " + i,
                            LastName = "Peters"
                        });
                        break;
                    case 1:
                        patients.Add(new Patient()
                        {
                            Steps = new List<Step>()
                            {
                                new Step()
                                {

                                    DateAdded = DateTime.Now,
                                    AlarmTime = DateTime.Now,
                                    StepName = getStepName(rand),
                                    IsCancled = true,
                                    Notes = new List<Note>() {new Note() { Text = "abc"} }
                                }
                            },
                            FirstName = "Bill" + i,
                            LastName = "Peters"
                        });
                        break;
                    case 2:
                        patients.Add(new Patient()
                        {
                            Steps = new List<Step>()
                            {
                                new Step()
                                {

                                    DateAdded = DateTime.Now,
                                    AlarmTime = DateTime.Now,
                                    StepName = getStepName(rand),
                                    IsCompleted = false,
                                    IsCancled = false,
                                    Notes = new List<Note>() {new Note() { Text = "abc"} }

                                }
                            },
                            FirstName = "Bill" + i,
                            LastName = "Peters"
                        });
                        break;
                }

            }


            context.Patients.AddRange(patients);
            context.SaveChanges();
            base.Seed(context);
        }

        private StepName getStepName(Random rand)
        {
            int number = rand.Next(1, 7);
            if (number == 1)
            {
                return StepName.Approved;
            }
            if (number == 2)
            {
                return StepName.Delivery;
            }
            if (number == 3)
            {
                return StepName.Exam;
            }
            if (number == 4)
            {
                return StepName.FollowUp;
            }
            if (number == 5)
            {
                return StepName.Impression;
            }
            return StepName.PaperWorkDone;
        }
    }
}
