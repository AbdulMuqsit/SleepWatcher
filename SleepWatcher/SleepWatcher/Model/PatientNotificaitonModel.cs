using SleepWatcher.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleepWatcher.Model
{
    class PatientNotificaitonModel : NotificationModel
    {
        private string _name;
        private StepName _stepName;
        private int _patientId;

        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(value, Name)) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public int PatientId
        {
            get { return _patientId; }
            set
            {
                if (Equals(value, PatientId)) return;
                _patientId = value;
                OnPropertyChanged();
            }
        }

        public StepName StepName
        {
            get { return _stepName; }
            set
            {
                if (Equals(value, StepName)) return;
                _stepName = value;
                OnPropertyChanged();
            }
        }
    }
}
