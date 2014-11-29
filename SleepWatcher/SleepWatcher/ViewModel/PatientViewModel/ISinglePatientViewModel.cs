using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface ISinglePatientViewModel : IViewModelBase
    {
        Patient Patient { get; set; }
        ActionCommand SwitchToAddPatientViewModelCommand { get; }
    }
}