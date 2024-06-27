using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SchnuppiDemoApp.Entities;
using SchnuppiDemoApp.Managers;
using SchnuppiDemoApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;


namespace SchnuppiDemoApp.WPF
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        private LoginWindowViewModel viewModel;
       

        public Models.User LoggedInUser = null;

        public LoginWindow()
        {
            viewModel = new LoginWindowViewModel();
            InitializeComponent();
            DataContext = viewModel;          
        }

        public void Login()
        {
            string userName = userEingabe.Text;
            string password = userPasswort.Password;

            var loggedInUser = viewModel.TryLogin(userName, password);

            if (loggedInUser != null)
            {
                LoggedInUser = loggedInUser;
                DialogResult = true;
            }
            else
            {              
                    this.ShowModalMessageExternal("He do stimmt was net", "User oder Passwort Falsch", MessageDialogStyle.Affirmative);                  
                    userEingabe.Text = "";
                    userPasswort.Password = "";               
            }
        }

        private void CMD_Login_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Login();
        }
        private void CMD_Login_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;

            if (userPasswort.Password + userEingabe.Text != "")
            {
                e.CanExecute = true;
            }
        }
    }
}