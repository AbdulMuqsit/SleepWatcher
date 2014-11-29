using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IPatientViewModel : IViewModelBase
    {
        ObservableCollection<Patient> Patients { get; }
        IViewModelBase CurrentViewModel { get; set; }
        ActionCommand SwitchToAddPatientViewCommmand { get; }
    }
}
