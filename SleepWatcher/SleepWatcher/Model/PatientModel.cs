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
        private ICollection<Step> _steps;
        private ICollection<Note> _notes;
        private RangeObservableCollection<StepModel> _stepModels = new RangeObservableCollection<StepModel>();
        private Step _currentStep;

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

        public int Id { get; set; }

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

        public PatientModel()
        {
            StepModels.CollectionChanged += StepModels_CollectionChanged;
        }

        private void StepModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("CurrentStep");
        }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

        [NotMapped]
        public StepModel CurrentStep
        {
            get { return StepModels.Last(); }
            
        }

        public virtual ICollection<Step> Steps
        {
            get { return _steps; }
            set
            {
                if(Equals(value,Steps))return;
                _steps = value;
              
                OnPropertyChanged();
                StepModels.Clear();
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                {
                    Mapper.CreateMap<Step, StepModel>();
                }
                StepModels.AddRange(Steps.Select(Mapper.Map<StepModel>));

            }
        }

        public virtual ICollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (Equals(value, Notes)) return;
                _notes = value;
                
                OnPropertyChanged();
            }
        }
    }
}
