using System.Collections.Generic;
using System.Windows.Documents;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface IPatientViewModel : IViewModelBase
    { 
        List<string> StepFilters { get;  }
        string SearchText { get; set; }
        string StepNameFilterString { get; set; }
        ActionCommand ShowWindowCommand { get; }
        ActionCommand ExitCommand { get; }
        RangeObservableCollection<PatientModel> Patients { get; set; }
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
        ActionCommand FilterCompleted { get; set; }
        ActionCommand FilterOngoing { get; set; }
        ActionCommand FilterOverdue { get; set; }
        ActionCommand ReverseSortCommand { get; set; }
        ActionCommand SearchCommand { get; set; }
        ActionCommand FilterStepCommand { get; set; }
    }
}
