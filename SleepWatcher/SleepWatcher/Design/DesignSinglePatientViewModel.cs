using System;
using System.Collections.Generic;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.ViewModel;

using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.Design
{
    class DesignSinglePatientViewModel : ISinglePatientViewModel, IViewModelBase
    {
        public Patient Patient
        {
            get
            {
                return new Patient
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step>()
                    {
                        new Step()
                        {
                            StepName = StepName.PaperWorkDone, AlarmTime = DateTime.Now, IsCompleted = true, DateAdded = DateTime.Now,
                            Notes =  new List<Note>()
                            {
                                new Note() {Text = "lorem ipsum adfhijkjsdkfjkajkfjiasdfjksjdfkjdskfjkajfsdiwejfiskjdkfj"},
                                new Note() {Text = "lorem ipsum adfhijkjsdkfjkajkfjiasdfjksjdfkjdskfjkajfsdiwejfiskjdkfj"}

                            }

                        },
                        new Step() { StepName = StepName.Approved, AlarmTime = DateTime.Now, IsCompleted = true, DateAdded = DateTime.Now},
                        new Step() { StepName = StepName.Exam, AlarmTime = DateTime.Now, IsCompleted = true , DateAdded = DateTime.Now},
                        new Step() { StepName = StepName.Impression, AlarmTime = DateTime.Now , IsCompleted = true, DateAdded = DateTime.Now},
                        new Step() { StepName = StepName.Delivery, AlarmTime = DateTime.Now , IsCompleted = true, DateAdded = DateTime.Now},
                        new Step() { StepName = StepName.FollowUp, AlarmTime = DateTime.Now, DateAdded = DateTime.Now},

                    }
                };

                //return null;
            }
            set { }
        }

        public ActionCommand SwitchToAddPatientViewModelCommand
        {
            get
            {
                return null;

            }
        }
    }
}