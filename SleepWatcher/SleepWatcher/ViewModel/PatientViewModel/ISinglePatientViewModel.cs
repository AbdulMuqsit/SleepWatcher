using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;
using SleepWatcher.Model;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface ISinglePatientViewModel : IViewModelBase
    {
        PatientModel Patient { get; set; }
        Model.StepModel SelectedStep { get; set; }
        Model.NoteModel SelectedNote { get; set; }
        ActionCommand SwitchToAddPatientViewModelCommand { get; }
        ActionCommand AddNewNoteCommand { get; }
        ActionCommand MarkCompleteCommand { get; }
        ActionCommand MarkCanceledCommand { get; }
        ActionCommand ClearView { get; }
        RangeObservableCollection<Model.StepModel> Steps { get; set; }
        RangeObservableCollection<Model.NoteModel> Notes { get; set; }
        ActionCommand MarkUnCanceledCommand { get; }
    }
}