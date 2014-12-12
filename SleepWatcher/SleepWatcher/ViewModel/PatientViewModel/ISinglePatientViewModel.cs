using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface ISinglePatientViewModel : IViewModelBase
    {
        bool IsBusy { get; set; }
        PatientModel Patient { get; set; }
        StepModel SelectedStep { get; set; }
        NoteModel SelectedNote { get; set; }
        ActionCommand SwitchToAddPatientViewModelCommand { get; }
        ActionCommand AddNewNoteCommand { get; }
        ActionCommand MarkCompleteCommand { get; }
        ActionCommand MarkCanceledCommand { get; }
        ActionCommand ClearView { get; }
        RangeObservableCollection<StepModel> Steps { get; set; }
        RangeObservableCollection<NoteModel> Notes { get; set; }
        ActionCommand MarkUnCanceledCommand { get; }
    }
}