using System.Collections.ObjectModel;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    public interface IPatientViewModel : IViewModelBase
    {
        ObservableCollection<Patient> Patients { get; }
        IViewModelBase CurrentViewModel { get; set; }
    }
}
