using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IPatientViewModel : IViewModelBase
    {
        ActionCommand ExitCommand { get; }
        RangeObservableCollection<PatientModel> Patients { get; }
        IViewModelBase CurrentViewModel { get; set; }
        ActionCommand SwitchToAddPatientViewCommmand { get; }
        bool IsBusy { get; set; }
        string BusyMessage { get; set; }
        ActionCommand GetOverDuePatientsCommand { get; set; }
        Patient Patient { get; set; }
        ActionCommand GetAllPatients { get; set; }
        RangeObservableCollection<PatientModel> OverDuePatients { get; set; }
        NotificationModel NotificationModel { get; set; }
        ActionCommand FilterPatientsCommand { get; set; }
        ActionCommand SubscribeNotificationsCommand { get; set; }
        ActionCommand FilterCnacled { get; set; }
        bool ShowOverDue { get; set; }
        bool ShowCanceled { get; set; }
        bool ShowCompleted { get; set; }
        bool ShowOngoing { get; set; }
    }
}
