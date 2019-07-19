using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using MorningReminder.Commands;
using MorningReminder.Views;
using Utils.Utilities;

namespace MorningReminder.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private ObservableCollection<ITaskViewModel> _taskViewModels;
        private RelayCommand _addTaskCommand;
        private RelayCommand _editTaskCommand;
        private RelayCommand _closeCommand;
        private RelayCommand _minimizeCommand;
        private RelayCommand _restoreWindowCommand;
        private IList<ITask> _taskList;

        public MainViewModel(IMessenger messenger = null)
        {
            WindowMessenger = messenger;
            WindowMessenger.RegisterAction(Messages.DELETE_TASK, (ITaskViewModel param) => DeleteTask(param));
            WindowMessenger.RegisterAction(Messages.ADD_TASK, (ITask task) => AddTask(task));
            WindowMessenger.RegisterAction(Messages.EDIT_TASK, (ITaskViewModel task) => EditTask(task));
            AddTaskVM = new AddTaskViewModel(WindowMessenger) { Visibility = Visibility.Hidden };
        }

        private void CreateTaskViewModel()
        {
            _taskViewModels = new ObservableCollection<ITaskViewModel>();

            _taskList = XmlLoader.Get.LoadTasksFromXmlFile().ToList();
            foreach (ITask tasks in _taskList)
            {
                _taskViewModels.Add(new TaskViewModel(tasks, WindowMessenger));
            }
        }

        public AddTaskViewModel AddTaskVM { get; set; }

        public ObservableCollection<ITaskViewModel> Tasks
        {
            get
            {
                if (_taskViewModels == null)
                {
                    CreateTaskViewModel();
                }

                return _taskViewModels;
            }
        }

        #region commands 

        public ICommand AddTaskCommand
        {
            get
            {
                return _addTaskCommand ?? (_addTaskCommand = new RelayCommand(param => ShowAddTask()));
            }
        }

        public ICommand EditTaskCommand
        {
            get
            {
                return _editTaskCommand ?? (_editTaskCommand = new RelayCommand(param => ShowAddTask()));
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new RelayCommand(param => App.Current.Shutdown(0)));
            }
        }

        public ICommand MinCommand
        {
            get
            {
                return _minimizeCommand ?? (_minimizeCommand = new RelayCommand(param => App.Current.MainWindow.WindowState = WindowState.Minimized));
            }
        }

        public ICommand MaxCommand
        {
            get
            {
                return _restoreWindowCommand ?? (_restoreWindowCommand = new RelayCommand(parm => RestoreWindowSize()));
            }
        }

        #endregion

        private static void RestoreWindowSize()
        {
            App.Current.MainWindow.WindowState = App.Current.MainWindow.WindowState == WindowState.Normal
                                                     ? WindowState.Maximized
                                                     : WindowState.Normal;
        }

        private void AddTask(ITask task)
        {
            Tasks.Add(new TaskViewModel(task, WindowMessenger));
            _taskList.Add(task);
            SaveToXml();
        }

        private void EditTask(ITaskViewModel taskViewModel)
        {
            var temp = Tasks.First(f => f.Equals(taskViewModel));
            Tasks.Remove(taskViewModel);
            _taskList.Remove(taskViewModel.Task);
            AddTaskVM.TaskDescription = temp.TaskDescription;
            AddTaskVM.TaskName = temp.TaskName;
            AddTaskVM.HasBeenEdited = true;
            ShowAddTask();
        }

        private void SaveToXml()
        {
            XmlLoader.Get.SaveXmlFile(_taskList);
        }

        private void DeleteTask(ITaskViewModel deleteItem)
        {
            Tasks.Remove(deleteItem);
            _taskList.Remove(deleteItem.Task);

            SaveToXml();
        }

        private void ShowAddTask()
        {
            AddTaskVM.Visibility = Visibility.Visible;
        }
    }
}