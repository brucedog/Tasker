using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using ViewModel.Commands;

namespace MorningReminder.ViewModel
{
    public class AddTaskViewModel : ViewModelBase, ITaskViewModel
    {
        private ICommand _saveCommand;
        private ICommand _closeCommand;
        private ITask _task;

        public AddTaskViewModel(IMessenger messenger)
        {
            WindowMessenger = messenger;
            _task = new Task();
        }

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

        public ICommand AddTaskCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(param => UpdateTasks(),
                    param => !string.IsNullOrEmpty(TaskName) || !string.IsNullOrEmpty(TaskDescription)));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(param => Close()));
            }
        }

        private void Close()
        {
            WindowMessenger.Notify(Messages.CLOSE_ADD_TASK_WINDOW);
        }

        private void UpdateTasks()
        {
            TaskName = string.Empty;
            TaskDescription = string.Empty;
            WindowMessenger.Notify(Messages.ADD_TASK, _task);
        }
    }
}