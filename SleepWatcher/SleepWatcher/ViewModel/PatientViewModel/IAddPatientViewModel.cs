using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IAddPatientViewModel : IViewModelBase
    {
        Patient Patient { get; set; }
        ActionCommand AddPatinetCommand { get; }
        ActionCommand SwitchToSinglePatientViewCommand { get; }
        bool StartFirstStep { get; set; }
    }
}