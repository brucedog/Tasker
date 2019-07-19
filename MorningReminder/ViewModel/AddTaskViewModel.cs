using System;
using System.Windows;
using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using MorningReminder.Commands;

namespace MorningReminder.ViewModel
{
    public class AddTaskViewModel : ViewModelBase, ITaskViewModel
    {
        private ICommand _saveCommand;
        private ICommand _closeCommand;
        private Visibility _visibility;
        private readonly ITask _task;
        private ITask _edittedTask;
        private bool _hasBeenEdited;

        public AddTaskViewModel(IMessenger messenger)
        {
            WindowMessenger = messenger;
            _task = new Task();
            Visibility = Visibility.Visible;
        }

        # region properties

        public string TaskName
        {
            get
            {
                return _task.Name;
            }

            set
            {
                _task.Name = value;
                NotifyPropertyChanged(() => TaskName);
            }
        }

        public string TaskDescription
        {
            get
            {
                return _task.Description;
            }

            set
            {
                _task.Description = value;
                NotifyPropertyChanged(() => TaskDescription);
            }
        }

        public ITask Task
        {
            get
            {
                return _task;
            }
        }

        public bool HasBeenEdited
        {
            private get { return _hasBeenEdited; }
            set
            {
                _hasBeenEdited = value;
                NotifyPropertyChanged(() => AddOrEdit);
            }
        }

        public string AddOrEdit { get { return HasBeenEdited ? "Update Task" : "Add Task"; } }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                if (_visibility == Visibility.Visible && HasBeenEdited)
                {
                    _edittedTask = _task;
                }
                NotifyPropertyChanged(() => Visibility);
            }
        }

        #endregion

        #region commands

        public ICommand AddTaskCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => UpdateTasks(),
                    param => !string.IsNullOrWhiteSpace(TaskName) || !string.IsNullOrWhiteSpace(TaskDescription)));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(param => Close()));
            }
        }

        #endregion

        private void Close()
        {
            if (HasBeenEdited)
            {
                WindowMessenger.Notify(Messages.ADD_TASK, new Task { Name = _edittedTask.Name, Description = _edittedTask.Description});
                ClearInput();
            }
            HasBeenEdited = false;
            Visibility = Visibility.Hidden;
        }

        private void UpdateTasks()
        {
            ITask addTask = new Task { Name = _task.Name, Description = _task.Description };

            if (HasBeenEdited)
            {
                WindowMessenger.Notify(Messages.ADD_TASK, addTask);
                _edittedTask = _task;
                HasBeenEdited = false;
                Close();
            }
            else
            {
                WindowMessenger.Notify(Messages.ADD_TASK, addTask);
                ClearInput();
            }
        }

        private void ClearInput()
        {
            TaskName = string.Empty;
            TaskDescription = string.Empty;
        }
    }
}