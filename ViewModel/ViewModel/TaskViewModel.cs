using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using MorningReminder.ViewModel;
using ViewModel.Commands;

namespace ViewModel.ViewModel
{
    public class TaskViewModel : ViewModelBase, ITaskViewModel
    {
        private ITask Atask;
        private RelayCommand _deleteTaskCommand;

        public TaskViewModel(ITask model, IMessenger messenger)
        {
            Atask = model;
            WindowMessenger = messenger;
        }

        public ICommand DeleteTaskCommand
        {
            get
            {
                return _deleteTaskCommand ?? (_deleteTaskCommand = new RelayCommand(param => WindowMessenger.Notify(Messages.DELETE_TASK, this)));
            }
        }

        public string TaskName
        {
            get
            {
                return Atask.Name;
            }
        }

        public string TaskDescription
        {
            get
            {
                return Atask.Description;
            }
        }

        public ITask Task
        {
            get
            {
                return Atask;
            }
        }
    }
}