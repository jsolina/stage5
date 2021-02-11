using System.Linq;
using System.Windows;
using WpfApp2.ViewModel;
using System.Windows.Input;
using Domain.Contracts;
using Domain.Models;

namespace WpfApp2.View
{
    /// <summary>
    /// Interaction logic for TaskListVIew.xaml
    /// </summary>
    public partial class TaskListView : Window
    {
        private ITaskList taskListDbContext;
        private IItem itemDbContext;
        string token;

        public TaskListView(ITaskList _taskListDbContext, IItem _itemDbContext, string _token)
        {
            this.taskListDbContext = _taskListDbContext;
            this.itemDbContext = _itemDbContext;
            token = _token;

            InitializeComponent();

            var viewModel = new TaskListViewModel(taskListDbContext, itemDbContext, token);
            DataContext = viewModel;
        }

        /*
        private void GetTasks()
        {
            var viewModel = new TaskListViewModel(taskListDbContext, itemDbContext, token);
            DataContext = viewModel;

        }

        private void View(object s, RoutedEventArgs e)
        {
            taskListModel = (s as FrameworkElement).DataContext as TaskListModel;
            ItemsView vi = new ItemsView(itemDbContext, taskListModel, token);
            vi.ShowDialog();
            GetTasks();
        }

        private void Update(object s, RoutedEventArgs e)
        {
            taskListModel = (s as FrameworkElement).DataContext as TaskListModel;
            MessageBox.Show(taskListModel.TaskName);
            AddUpdateTaskView vi = new AddUpdateTaskView(taskListDbContext, taskListModel, "Update", token);
            vi.ShowDialog();
            GetTasks();
        }

        private void Add(object s, RoutedEventArgs e)
        {
            AddUpdateTaskView vi = new AddUpdateTaskView(taskListDbContext, taskListModel, "Add", token);
            vi.ShowDialog();
            GetTasks();
        }


        private void Delete(object s, RoutedEventArgs e)
        {
            var rowToBeDeleted = (s as FrameworkElement).DataContext as TaskListModel;
            taskListDbContext.Remove(rowToBeDeleted, token);
            GetTasks();
            MessageBox.Show(rowToBeDeleted.TaskName + " has been Deleted", "Task Deleted!");
        }
        */
    }
}
