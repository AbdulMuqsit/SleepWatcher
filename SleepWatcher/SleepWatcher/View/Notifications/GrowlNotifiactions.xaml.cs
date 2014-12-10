using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SleepWatcher.Model;
using SleepWatcher.ViewModel;

namespace SleepWatcher.View.Notifications
{
    public partial class GrowlNotifiactions
    {
        private const byte MAX_NOTIFICATIONS = 4;
        private int count;
        public Model.Notifications Notifications = new Model.Notifications();
        private readonly Model.Notifications buffer = new Model.Notifications();

        public GrowlNotifiactions()
        {
            InitializeComponent();
            NotificationsControl.DataContext = Notifications;
        }

        public void AddNotification(NotificationModel notificationModel)
        {
            notificationModel.Id = count++;
            if (Notifications.Count + 1 > MAX_NOTIFICATIONS)
                buffer.Add(notificationModel);
            else
                Notifications.Add(notificationModel);
            
            //Show window if there're notifications
            if (Notifications.Count > 0 && !IsActive)
                Show();
        }

        public void RemoveNotification(NotificationModel notificationModel)
        {
            if (Notifications.Contains(notificationModel))
                Notifications.Remove(notificationModel);
            
            if (buffer.Count > 0)
            {
                Notifications.Add(buffer[0]);
                buffer.RemoveAt(0);
            }
            
            //Close window if there's nothing to show
            if (Notifications.Count < 1)
                Hide();
        }

        private void NotificationWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height != 0.0)
                return;
            var element = sender as Grid;
            RemoveNotification(Notifications.First(n => n.Id == Int32.Parse(element.Tag.ToString())));
        }
    }
}
