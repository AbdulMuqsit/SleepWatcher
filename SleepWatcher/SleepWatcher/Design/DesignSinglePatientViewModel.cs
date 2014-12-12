using System;
using System.Collections.Generic;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using SleepWatcher.ViewModel;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.Design
{
    internal class DesignSinglePatientViewModel : ISinglePatientViewModel, IViewModelBase
    {
        public bool IsBusy { get; set; }

        public PatientModel Patient
        {
            get
            {
                return new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    StepModels = new RangeObservableCollection<StepModel>
                    {
                        new StepModel
                        {
                            StepName = StepName.PaperWorkDone,
                            AlarmTime = DateTime.Now,
                            DateAdded = DateTime.Now,
                            Notes = new List<Note>
                            {
                                new Note {Text = "lorem ipsum adfhijkjsdkfjkajkfjiasdfjksjdfkjdskfjkajfsdiwejfiskjdkfj"},
                                new Note {Text = "lorem ipsum adfhijkjsdkfjkajkfjiasdfjksjdfkjdskfjkajfsdiwejfiskjdkfj"}
                            }
                        },
                        new StepModel
                        {
                            StepName = StepName.Approved,
                            AlarmTime = DateTime.Now,
                            IsCancled = true,
                            DateAdded = DateTime.Now
                        },
                        new StepModel
                        {
                            StepName = StepName.Exam,
                            AlarmTime = DateTime.MinValue,
                            DateAdded = DateTime.Now
                        },
                        new StepModel
                        {
                            StepName = StepName.Impression,
                            AlarmTime = DateTime.MaxValue,
                            DateAdded = DateTime.Now
                        },
                        new StepModel
                        {
                            StepName = StepName.Delivery,
                            AlarmTime = DateTime.MaxValue,
                            IsCompleted = true,
                            DateAdded = DateTime.Now
                        },
                        new StepModel {StepName = StepName.FollowUp, AlarmTime = DateTime.Now, DateAdded = DateTime.Now}
                    }
                };

                //return null;
            }
            set { }
        }

        public StepModel SelectedStep
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public NoteModel SelectedNote
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand SwitchToAddPatientViewModelCommand
        {
            get { return null; }
        }

        public ActionCommand AddNewNoteCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ActionCommand MarkCompleteCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ActionCommand MarkCanceledCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ActionCommand ClearView
        {
            get { throw new NotImplementedException(); }
        }

        public RangeObservableCollection<StepModel> Steps
        {
            get { return null; }
            set { throw new NotImplementedException(); }
        }

        public RangeObservableCollection<NoteModel> Notes
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand MarkUnCanceledCommand
        {
            get { throw new NotImplementedException(); }
        }
    }
}