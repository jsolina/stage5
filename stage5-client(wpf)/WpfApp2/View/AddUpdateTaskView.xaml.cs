using Domain.Contracts;
using Domain.Models;
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
    /// Interaction logic for CreateUpdateTask.xaml
    /// </summary>
    public partial class AddUpdateTaskView : Window
    {
        private ITaskList taskListDbContext;
        TaskListModel selectedRow = new TaskListModel();
        string token;

        public AddUpdateTaskView(ITaskList _taskListDbContext, TaskListModel _selectedRow, string _updateOrAdd, string _token)
        {
            InitializeComponent();
            token = _token;
            this.taskListDbContext = _taskListDbContext;
            if (_updateOrAdd == "Update")
            {
                selectedRow = _selectedRow;
            }

            this.DataContext = new AddUpdateTaskViewModel(taskListDbContext, selectedRow, _updateOrAdd, token);


            var viewModel = new AddUpdateTaskViewModel(taskListDbContext, selectedRow, _updateOrAdd, token);
            DataContext = viewModel;
        }

    }
}
