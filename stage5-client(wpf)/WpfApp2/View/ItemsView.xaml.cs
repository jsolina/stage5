using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for ViewItems.xaml
    /// </summary>
    public partial class ItemsView : Window
    {
        IItem dbContext;
        TaskListModel selectedRowTask = new TaskListModel();
        ItemModel selectedRowItem = new ItemModel();

        string token;

        public ItemsView(IItem _dbContext, TaskListModel _selectedRowTask, string _token)
        {
            InitializeComponent();
            this.dbContext = _dbContext;
            selectedRowTask = _selectedRowTask;
            headerItemName.Text = "Item List of Task: '" + selectedRowTask.TaskName + "'";
            token = _token;

            var viewModel = new ItemsViewModel(_dbContext, selectedRowTask, token);
            this.DataContext = viewModel;
        }
    }
}
