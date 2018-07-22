using PasswordStore.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PasswordStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        [STAThread]
        public static void Main()
        {
            if (SingleInstance<App>.InitializeAsFirstInstance(Constants.ApplicationName))
            {
                var application = new App();
                application.InitializeComponent();
                application.Run();
   
                SingleInstance<App>.Cleanup();
            }
        }

        public App()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            MessageBox.Show($"UnhandledException caught {ex.Message}: UnhandledException StackTrace : {ex.StackTrace} Runtime terminating: {e.IsTerminating}", "Error");
        }

        public bool SignalExternalCommandLineArgs(IList<string> args)
        {
            return true;
        }
    }
}
