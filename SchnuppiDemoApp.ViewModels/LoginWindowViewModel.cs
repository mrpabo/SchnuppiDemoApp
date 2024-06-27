




using SchnuppiDemoApp.Managers;
using SchnuppiDemoApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SchnuppiDemoApp.ViewModels // änderungen von view und für model?
{
    public class LoginWindowViewModel : INotifyPropertyChanged
    {
        #region Private variables

        UsersManager usersManager = new UsersManager();

        #endregion

        #region Constructor

        public LoginWindowViewModel()
        {

        }

        #endregion

        #region Public Methods

    
        public User TryLogin(string userName, string password)
        {
            return usersManager.TryLogin(userName, password);
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }

       

        #endregion
    }
}
