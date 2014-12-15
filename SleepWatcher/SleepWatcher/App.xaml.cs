using System;
using System.Windows;
using SleepWatcher.Infrastructure;

namespace SleepWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AutoMapperConfiguration.Configure();
            AppDomain.CurrentDomain.UnhandledException += (s,k)=>MessageBox.Show("Something went wrong!\nMessage: "+((Exception)(k.ExceptionObject)).Message);
        }

        
    }
}
