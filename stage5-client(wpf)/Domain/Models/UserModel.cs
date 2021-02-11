using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Models
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        /*
        public string Username
        {
            get { return _Username; }
            set
            {
                if (value != _Username)
                {
                    _Username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _Username; }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        //para mag update yung changes
        #region INotifyPropertyChanged Members  

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        */
    }


}
