using SchnuppiDemoApp.WPF;
using System.Windows;


namespace SchnuppiDemoApp
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if(loginWindow.DialogResult == true)
            {
                loginWindow.Close();

                MainWindow mainWindow = new MainWindow(loginWindow.LoggedInUser);
                mainWindow.Show();

                Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            }
            else
            {
                Application.Current.Shutdown();
            }
            base.OnStartup(e);
        }
    }
}

