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
    /// Interaction logic for CreateUpdateItem.xaml
    /// </summary>
    public partial class AddUpdateItemView : Window
    {

        IItem dbContext;
        ItemModel selectedRowItem = new ItemModel();
        TaskListModel selectedRowTask = new TaskListModel();
        string token;

        public AddUpdateItemView(IItem _dbContext, TaskListModel _selectedRowTask, ItemModel _selectedRowItem, string _updateOrAdd, string _token)
        {
            InitializeComponent();
            token = _token;

            this.dbContext = _dbContext;
            selectedRowTask = _selectedRowTask;

            if (_updateOrAdd == "Update")
            {
                selectedRowItem = _selectedRowItem;
            }

            this.DataContext = new AddUpdateItemViewModel(dbContext, selectedRowTask, selectedRowItem, _updateOrAdd, token);

             var viewModel = new AddUpdateItemViewModel(dbContext, selectedRowTask, selectedRowItem, _updateOrAdd, token);
             DataContext = viewModel;
        }

    }
}
