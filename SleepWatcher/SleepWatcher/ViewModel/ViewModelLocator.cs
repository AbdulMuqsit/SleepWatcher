using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SleepWatcher.Design;
using SleepWatcher.ViewModel.PatientViewModel;

namespace SleepWatcher.ViewModel
{
    public  class ViewModelLocator
    {
        public IPatientViewModel PatientViewModel { get; set; }
        public IAddPatientViewModel AddPatientViewModel { get; set; }
        public ISinglePatientViewModel SinglePatientViewModel { get; set; }


        public ViewModelLocator()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                PatientViewModel = new DesignPatientViewModel();
                //   AddPatientViewModel = new DesignAddPatientViewModel();
                //   SinglePatientViewModel = new DesignSinglePatientViewModel();
            }
            else
            {
                PatientViewModel = new PatientViewModel.PatientViewModel();
            }
            AddPatientViewModel = new AddPatientViewModel();
            SinglePatientViewModel = new SinglePatientViewModel();
        }
    }
}
