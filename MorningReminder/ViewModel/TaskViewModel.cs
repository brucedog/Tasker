using System.Windows;
using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using MorningReminder.Commands;

namespace MorningReminder.ViewModel
{
    public class TaskViewModel : ViewModelBase, ITaskViewModel
    {
        private readonly ITask _atask;
        private RelayCommand _deleteTaskCommand;
        private RelayCommand _editTaskCommand;

        public TaskViewModel(ITask model, IMessenger messenger)
        {
            _atask = model;
            WindowMessenger = messenger;
        }

        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(param => WindowMessenger.Notify(Messages.DELETE_TASK, this)));
            }
        }

        public ICommand EditTaskCommand
        {
            get
            {
                return _editTaskCommand ?? (_editTaskCommand = new RelayCommand(param => WindowMessenger.Notify(Messages.EDIT_TASK, this)));
            }
        }

        public string TaskName
        {
            get
            {
                return _atask.Name;
            }
            set { _atask.Name = value; }
        }

        public string TaskDescription
        {
            get
            {
                return _atask.Description;
            }
            set { _atask.Description = value; }
        }

        public ITask Task
        {
            get
            {
                return _atask;
            }
        }

        public Visibility Visibility { get; set; }
    }
}