using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows;
using AutoMapper;
using SleepWatcher.Entites;
using SleepWatcher.Infrastructure;

namespace SleepWatcher.Model
{
    public class PatientModel: ObjectBase
    {
        private string _firstName;
        private string _lastName;

        private RangeObservableCollection<StepModel> _stepModels = new RangeObservableCollection<StepModel>();
        private StepModel _currentStep;
        private int _id;

        public RangeObservableCollection<StepModel> StepModels
        {
            get { return _stepModels; }
            set
            {
                if(Equals(value,StepModels))
                return;
                _stepModels = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return _id; }
            set
            {
                if (Equals(value, _id)) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (Equals(value, FirstName)) return;
                _firstName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");

            }
        }

        [Required]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (Equals(value, LastName)) return;
                _lastName = value;
                OnPropertyChanged();
                OnPropertyChanged("FullName");
            }
        }

      
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        [NotMapped]
        public StepModel CurrentStep
        {
            get { return _currentStep; }
            set
            {
                if (Equals(value, CurrentStep))
                    return;
                _currentStep = value;
                OnPropertyChanged();
            } 
        }
       
    }
}
