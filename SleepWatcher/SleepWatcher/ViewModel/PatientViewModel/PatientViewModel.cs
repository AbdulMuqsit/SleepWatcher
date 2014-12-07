using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    internal class PatientViewModel : ViewModelBase, IPatientViewModel
    {
        private IViewModelBase _currentViewModel;
        private Patient _patient = new Patient();
        private RangeObservableCollection<PatientModel> _patients;

        public PatientViewModel()
        {
            
            CurrentViewModel = new SinglePatientViewModel();
            //initiate switch to add pateint view model command
            SwitchToAddPatientViewCommmand = new ActionCommand(() => { CurrentViewModel = Locator.AddPatientViewModel; });
            //initiate get all patients command
            GetAllPatients =
                new ActionCommand(
                     async () =>
                    {
                        Task task = new Task(async() =>
                        {
                            Patients =
                            new RangeObservableCollection<PatientModel>(
                                (await Context.Patients.Include(e=>e.Steps).ToListAsync()).Select(Mapper.Map<Patient, PatientModel>));
                        });
                        task.Start();
                        await Task.WhenAll(task);
                        var pa = Patients;
                        Debug.Write(pa.Count);
                    });

            //getting patients from database to populate listbox
            GetAllPatients.Execute(null);
           
        }

        public Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand GetAllPatients { get; }

        public RangeObservableCollection<PatientModel> Patients
        {
            get { return _patients; }
            set
            {
                if (Equals(value, Patients)) return;
                _patients = value;
                OnPropertyChanged();
            }
        }

        public ActionCommand SwitchToAddPatientViewCommmand { get; }

        public IViewModelBase CurrentViewModel
        {
            get { return _currentViewModel; }
            set
            {
                if (Equals(value, CurrentViewModel)) return;
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}