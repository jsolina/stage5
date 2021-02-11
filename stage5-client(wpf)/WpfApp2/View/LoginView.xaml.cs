using Domain.Contracts;
using Domain.Models;
using Infrastracture.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.ViewModel;

namespace WpfApp2.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private ITaskList taskListDbContext;
        private IItem itemDbContext;
        public UserModel userModels = new UserModel();
        RequestToken requestToken = new RequestToken();

        public LoginView(ITaskList _taskListDbContext, IItem _itemDbContext)
        {
            this.taskListDbContext = _taskListDbContext;
            this.itemDbContext = _itemDbContext;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                userModels.Username = username.Text;
                userModels.Password = password.Password;
                    
                var token = requestToken.TokenRequest(userModels);

                TaskListView tv = new TaskListView(taskListDbContext, itemDbContext, token);
                tv.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Credentials \n \n" + "Exception: \n" + ex.Message , "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
