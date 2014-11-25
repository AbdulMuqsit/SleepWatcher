using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    public interface ISinglePatientViewModel : IViewModelBase
    {
        Patient Patient { get; set; }
    }
}