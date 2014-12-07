using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IPatientViewModel : IViewModelBase
    {
        RangeObservableCollection<PatientModel> Patients { get; }
        IViewModelBase CurrentViewModel { get; set; }
        ActionCommand SwitchToAddPatientViewCommmand { get; }
    }
}
