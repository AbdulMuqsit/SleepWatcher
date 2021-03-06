﻿using System;
using System.Collections.Generic;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;
using SleepWatcher.ViewModel;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.Design
{
    public class DesignPatientViewModel : ViewModelBase, IPatientViewModel
    {
        public List<string> StepFilters { get; set; }
        public string SearchText { get; set; }
        public string StepNameFilterString { get; set; }

        public ActionCommand ShowWindowCommand
        {
            get { throw new NotImplementedException(); }
        }

        public ActionCommand ExitCommand
        {
            get { throw new NotImplementedException(); }
        }

        public RangeObservableCollection<PatientModel> Patients { get; set; }
        public IViewModelBase CurrentViewModel { get; set; }

        public ActionCommand SwitchToAddPatientViewCommmand { get; set; }

        public bool IsBusy
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public string BusyMessage
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand GetOverDuePatientsCommand
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public Patient Patient { get; set; }

        public ActionCommand GetAllPatients
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public RangeObservableCollection<PatientModel> OverDuePatients
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public NotificationModel NotificationModel
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public ActionCommand FilterPatientsCommand
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public ActionCommand SubscribeNotificationsCommand
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public ActionCommand FilterCnacled
        {
            get { throw new System.NotImplementedException(); }
            set { throw new System.NotImplementedException(); }
        }

        public bool ShowOverDue
        {
            get { return true; }
            set { throw new System.NotImplementedException(); }
        }

        public bool ShowCanceled
        {
            get { return true; }
            set { throw new System.NotImplementedException(); }
        }

        public bool ShowCompleted
        {
            get { return true; }
            set { throw new System.NotImplementedException(); }
        }

        public bool ShowOngoing
        {
            get { return true; }
            set { throw new System.NotImplementedException(); }
        }

        public ActionCommand FilterCompleted
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand FilterOngoing
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand FilterOverdue
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public ActionCommand ReverseSortCommand { get; set; }
        public ActionCommand SearchCommand { get; set; }
        public ActionCommand FilterStepCommand { get; set; }

        public DesignPatientViewModel()
        {
            CurrentViewModel = new DesignSinglePatientViewModel();
            Patient = new Patient
            {
                FirstName = "Patient",
                LastName = "Kzam",
                Steps = new List<Step> { new Step { StepName = StepName.Approved } }
            };


            Patients = new RangeObservableCollection<PatientModel>
            {
                new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved,AlarmTime = DateTime.MaxValue}}
                },
                 new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Delivery, AlarmTime = DateTime.MinValue}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved, IsCompleted = true}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved, AlarmTime = DateTime.MaxValue}}
                },
                 new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Delivery, IsCancled=true}}
                },
                  new PatientModel
                {
                    FirstName = "Patient",
                    LastName = "Kzam",
                    Steps = new List<Step> {new Step {StepName = StepName.Approved, IsCompleted = true}}
                }
            };


        }

    }
}
