using System.Collections.ObjectModel;
using Microsoft.Expression.Interactivity.Core;
using SleepWatcher.Entites;
using Step = SleepWatcher.Model.Step;

namespace SleepWatcher.ViewModel.PatientViewModel
{
    public interface ISinglePatientViewModel : IViewModelBase
    {
        Patient Patient { get; set; }
        Model.Step SelectedStep { get; set; }
        Model.Note SelectedNote { get; set; }
        ActionCommand SwitchToAddPatientViewModelCommand { get; }
        ActionCommand AddNewNoteCommand { get; }
        ActionCommand MarkCompleteCommand { get; }
        ActionCommand MarkCanceledCommand { get; }
        ActionCommand ClearView { get; }
        ObservableCollection<Model.Step> Steps { get; set; }
        ObservableCollection<Model.Note> Notes { get; set; }
        ActionCommand MarkUnCanceledCommand { get; }
    }
}