using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IAddPatientViewModel : IViewModelBase
    {
        bool IsBusy { get; set; }
        string BusyMessage { get; set; }
        Patient Patient { get; set; }
        ActionCommand AddPatinetCommand { get; }
        ActionCommand SwitchToSinglePatientViewCommand { get; }
        bool StartFirstStep { get; set; }
    }
}