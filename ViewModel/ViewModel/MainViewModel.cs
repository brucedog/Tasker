using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using InterfaceLibrary.Interface;
using Messenger;
using Model.Models;
using MorningReminder.ViewModel;
using MorningReminder.Views;
using Utils.Utilities;
using ViewModel.Commands;

namespace ViewModel.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private ObservableCollection<ITaskViewModel> _taskViewModels;
        private RelayCommand _addTaskCommand;
        private RelayCommand _closeCommand;
        private RelayCommand _minimizeCommand;
        private RelayCommand _restoreWindowCommand;
        private IList<ITask> _taskList;

        public MainViewModel(IMessenger messenger = null)
        {
            WindowMessenger = messenger;
            WindowMessenger.RegisterAction(Messages.DELETE_TASK, (ITaskViewModel param) => DeleteTask(param));
            WindowMessenger.RegisterAction(Messages.CLOSE_ADD_TASK_WINDOW, ResetMainWindow);
            WindowMessenger.RegisterAction(Messages.CLOSE_CHANGE_ALARM_WINDOW, ResetMainWindow);
            WindowMessenger.RegisterAction(Messages.ADD_TASK, (ITask task) => AddTask(task));
        }

        private void CreateTaskViewModel()
        {
            _taskViewModels = new ObservableCollection<ITaskViewModel>();

            IXmlLoader xmlLoader = new XmlLoader();
            _taskList = xmlLoader.LoadTasksFromXmlFile().ToList();
            foreach (ITask tasks in _taskList)
            {
                _taskViewModels.Add((ITaskViewModel)new TaskViewModel(tasks, WindowMessenger));
            }
        }

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

        public ICommand AddTaskCommand
        {
            get
            {
                return _addTaskCommand ?? (_addTaskCommand = new RelayCommand(param => ShowAddTask()));
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

        private void SaveToXml()
        {
            IXmlLoader xmlLoader = new XmlLoader();
            xmlLoader.SaveXmlFile(_taskList);
        }

        private void DeleteTask(ITaskViewModel deleteItem)
        {
            Tasks.Remove(deleteItem);
            _taskList.Remove(deleteItem.Task);

            SaveToXml();
        }

        private void ShowAddTask()
        {
            AddTask addTask = new AddTask { DataContext = new AddTaskViewModel(WindowMessenger) };
            addTask.Owner = App.Current.MainWindow;
            App.Current.MainWindow = addTask;

            addTask.ShowDialog();
        }

        private static void ResetMainWindow()
        {
            var temp = App.Current.MainWindow.Owner;
            App.Current.MainWindow.Close();
            App.Current.MainWindow = temp;
        }
    }
}