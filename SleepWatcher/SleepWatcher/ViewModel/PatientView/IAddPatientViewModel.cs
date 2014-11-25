using SleepWatcher.Entites;

namespace SleepWatcher.ViewModel.PatientView
{
    public interface IAddPatientViewModel : IViewModelBase
    {
        Patient Patient { get; set; }
    }
}