using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Data;
using WpfApp2.Commnds;
using WpfApp2.View;
using System.Windows;
using Domain.Models;
using Domain.Contracts;
using Infrastracture.Persistence;

namespace WpfApp2.ViewModel
{
    public class TaskListViewModel : INotifyPropertyChanged
    {

        private IEnumerable<TaskListModel> taskList;
        private TaskListModel taskListSelectedRow = new TaskListModel();
        ITaskList taskListDbContext;
        IItem itemDbContext;

        string token;
        //public UserModel userModels = new UserModel();
        //RequestToken requestToken = new RequestToken();

        public TaskListViewModel(ITaskList _taskListDbContext, IItem _itemDbContext, string _token)
        {
            this.taskListDbContext = _taskListDbContext;
            this.itemDbContext = _itemDbContext;
            token = _token;

            GetData();
        }

        public void GetData()
        {
            TaskLists = taskListDbContext.FindAll(token).OrderByDescending(i => i.Id).ToList();
        }

        public IEnumerable<TaskListModel> TaskLists
        {
            get { return taskList; }
            set 
            {
                if (value != taskList)
                {
                    taskList = value;
                    NotifyPropertyChanged("TaskLists");
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

        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(p => Delete(p));
            }
        }

        public RelayCommand UpdateCommands
        {
            get
            {
                return new RelayCommand(p => Updates(p));
            }
        }

        public RelayCommand AddCommands
        {
            get
            {
                return new RelayCommand(p => Add(p));
            }
        }
        public RelayCommand ViewCommands
        {
            get
            {
                return new RelayCommand(p => View(p));
            }
        }

        public void Delete(object o)
        {
            taskListSelectedRow = taskListDbContext.FindById(Convert.ToInt32(o), token);
            taskListDbContext.Remove(taskListSelectedRow, token);
            MessageBox.Show(taskListSelectedRow.TaskName + " has been Deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
            GetData();
        }

        public void Updates(object o)
        {
            taskListSelectedRow = taskListDbContext.FindById(Convert.ToInt32(o), token);
            AddUpdateTaskView cuo = new AddUpdateTaskView(taskListDbContext, taskListSelectedRow, "Update", token);
            cuo.ShowDialog();
            GetData();
        }

        public void Add(object o)
        {
            AddUpdateTaskView cuo = new AddUpdateTaskView(taskListDbContext, taskListSelectedRow, "Add", "token");
            cuo.ShowDialog();
            GetData();

        }

        public void View(object o)
        {
            taskListSelectedRow = taskListDbContext.FindById(Convert.ToInt32(o), token);
            ItemsView vi = new ItemsView(itemDbContext, taskListSelectedRow, token);
            vi.ShowDialog();
            GetData();
  
        }

        public class RelayCommand : ICommand
        {
            private Action<object> action;
            private Func<bool> canFuncPerform;
            public event EventHandler CanExecuteChanged;
            public RelayCommand(Action<object> executeMethod)
            {
                action = executeMethod;
            }
            public RelayCommand(Action<object> executeMethod, Func<bool> canExecuteMethod)
            {
                action = executeMethod;
                canFuncPerform = canExecuteMethod;
            }
            public void RaiseCanExecuteChanged()
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
            public bool CanExecute(object parameter)
            {
                if (canFuncPerform != null)
                {
                    return canFuncPerform();
                }

                if (action != null)
                {
                    return true;
                }

                return false;
            }

            public void Execute(object parameter)
            {
                if (action != null)
                {
                    action(parameter);
                }
            }
        }

    }
}
