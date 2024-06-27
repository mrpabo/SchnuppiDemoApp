using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using OfficeOpenXml;
using SchnuppiDemoApp.ViewModels;
using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace SchnuppiDemoApp.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private MainWindowViewModel viewModel;

        public bool UserIsAdministrator { get; set; } // Hier setzen Sie den Benutzerstatus

        public MainWindow(Models.User loggedInUser)
        {
            // Füge den Lizenzkontext hinzu
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Oder LicenseContext.Commercial, je nach Lizenz?

            // Initialisiere ViewModel und andere Komponenten
            viewModel = new MainWindowViewModel(loggedInUser);
            InitializeComponent();
            DataContext = this.viewModel;
        }

        private void CMD_MainView_Quit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void CMD_MainView_Quit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_Search_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.Search();
        }
        private void CMD_Search_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_ZuCSVExportiern_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string filePath = "D:\\CSV-Datei.csv";
            viewModel.ExportToCSV(viewModel.SearchResults.ToList(), filePath);

            this.ShowModalMessageExternal("Daten wurden in CSV exportiert.", "Erfolg", MessageDialogStyle.Affirmative);
        }
        private void CMD_ZuCSVExportiern_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_ZuExelExportieren_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string filePath = "D:\\Excel-Datei.xlsx";
            viewModel.ExportToExcel(viewModel.SearchResults.ToList(), filePath);

            this.ShowModalMessageExternal("Daten wurden in Excel exportiert.", "Erfolg", MessageDialogStyle.Affirmative);

        }
        private void CMD_ZuExelExportieren_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_BenutzerWechseln_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (loginWindow.LoggedInUser != null)
            {
                viewModel.LoggedInUser = loginWindow.LoggedInUser;
            }
        }
        private void CMD_BenutzerWechseln_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_AktuelleZeileKopieren_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.CopyButton_Click();
        }
        private void CMD_AktuelleZeileKopieren_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (viewModel.SelectedRow != null)
            {
                e.CanExecute = true;
            }
        }

        private void CMD_AllesKopieren_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            viewModel.CopyAllButton();
        }
        private void CMD_AllesKopieren_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CMD_InfoAnzeigen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            DateTime current = DateTime.Now;
            this.ShowModalMessageExternal("HQ Nüziders \n" + DateTime.Now, "Miar sen die aller aller beschtan uf dr Welt!", MessageDialogStyle.Affirmative);
        }
        private void CMD_InfoAnzeigen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void datagridhome_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            viewModel.RowEditEnded((Models.LogData)e.Row.Item);
        }
    }
}
