using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using WpfApp2.Commnds;

namespace WpfApp2.ViewModel
{
    public class AddUpdateTaskViewModel : INotifyPropertyChanged
    {
        ITaskList dbContext;
        public TaskListModel taskListModels = new TaskListModel();
        public TaskListModelSelected taskListModelSelected = new TaskListModelSelected();

        //Command binding property for Button Click
        public SubmitCommandBinding SubmitClick { get; set; }

        string updateOrAdd;
        string token;
        string idempotencyKey;

        public AddUpdateTaskViewModel(ITaskList _dbContext, TaskListModel _TaskList, string _updateOrAdd, string _token)
        {
            idempotencyKey = Guid.NewGuid().ToString();

            SubmitClick = new SubmitCommandBinding(ShowMessage);
            SubmitClick.IsEnabled = true;

            this.dbContext = _dbContext;

            TaskListModels = _TaskList;

            //gumamit ako ng panibagong model para hindi mag autoBind
            //pag ginamit ko kasi yung 'TaskListModels' para sa selectedRow ko, nag chachange yung dataValue sa DataGrid kahit hindi pa nag ttrigger uupdate
            TaskListModelSelecteds.TaskName = TaskListModels.TaskName;
            TaskListModelSelecteds.TaskDetails = TaskListModels.TaskDetails;
            TaskListModelSelecteds.Email = TaskListModels.Email;

            updateOrAdd = _updateOrAdd;
            token = _token;
        }

        public TaskListModel TaskListModels
        {
            get { return taskListModels; }
            set
            {
                taskListModels = value;
                NotifyPropertyChanged("TaskListModels");
            }
        }

        public TaskListModelSelected TaskListModelSelecteds
        {
            get { return taskListModelSelected; }
            set
            {
                taskListModelSelected = value;
                NotifyPropertyChanged("TaskListModelSelecteds");
            }
        }

        /// <summary>
        /// Method to be called when property(value) is changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// Property changed Event 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// Actual method for display
        /// for button click event
        /// </summary>
        private void ShowMessage()
        {
            //change value to give notification of the click
            NotifyPropertyChanged("TaskListModels");

            if (TaskListModelSelecteds.TaskName != null) 
            {
                //Update
                if (updateOrAdd == "Update")
                {
                    TaskListModels.TaskName = TaskListModelSelecteds.TaskName;
                    TaskListModels.TaskDetails = TaskListModelSelecteds.TaskDetails;
                    TaskListModels.Email = TaskListModelSelecteds.Email;

                    dbContext.Update(TaskListModels, token);
                    MessageBox.Show("Task has been Updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                //Add
                else
                {

                    if (TaskListModelSelecteds.TaskName == TaskListModels.TaskName
                        && TaskListModelSelecteds.TaskDetails == TaskListModels.TaskDetails
                        && TaskListModelSelecteds.Email == TaskListModels.Email)
                    {
                        MessageBox.Show("Duplication of Data!!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        TaskListModels.TaskName = TaskListModelSelecteds.TaskName;
                        TaskListModels.TaskDetails = TaskListModelSelecteds.TaskDetails;
                        TaskListModels.Email = TaskListModelSelecteds.Email;

                        dbContext.CreateWitKey(TaskListModels, token, idempotencyKey);
                        MessageBox.Show("Task has been Added", "Added", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Task Name cannot be null/empty", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
