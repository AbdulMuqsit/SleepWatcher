using System.ComponentModel;
using System.Windows;
using Expression.Blend.SampleData.SingleNoteViewSample;
using SleepWatcher.Design;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.ViewModel
{
    public class ViewModelLocator
    {
        public IPatientViewModel PatientViewModel { get; set; }
        public IAddPatientViewModel AddPatientViewModel { get; set; }
        public ISinglePatientViewModel SinglePatientViewModel { get; set; }
        public OverDuePatientsViewModel OverDuePatientsViewModel { get; set; }
        public NotesViewModel NotesViewModel { get; set; }
        public SingleNoteViewModel SingleNoteViewModel { get; set; }
        public AddPatientButtonViewModel AddPatientButtonViewModel { get; set; }

        public ViewModelLocator()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                PatientViewModel = new DesignPatientViewModel();
                AddPatientViewModel = new DesignAddPatientViewModel();
                SinglePatientViewModel = new DesignSinglePatientViewModel();
            }
            else
            {
                PatientViewModel = new PatientViewModel.PatientViewModel();
                AddPatientViewModel = new AddPatientViewModel();
                SinglePatientViewModel = new SinglePatientViewModel();
                OverDuePatientsViewModel = new OverDuePatientsViewModel();
                SingleNoteViewModel = new SingleNoteViewModel();
                NotesViewModel = new NotesViewModel();
                AddPatientButtonViewModel = new AddPatientButtonViewModel();
            }

        }
    }
}
