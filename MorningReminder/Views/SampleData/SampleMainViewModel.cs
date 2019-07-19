using System;
using System.Collections.ObjectModel;
using InterfaceLibrary.Interface;
using Model.Models;
using MorningReminder.ViewModel;

namespace MorningReminder.Views.SampleData
{
    public class SampleMainViewModel : IMainViewModel
    {
        public SampleMainViewModel()
        {
            ObservableCollection<ITaskViewModel> taskViewModel = new ObservableCollection<ITaskViewModel>();
            taskViewModel.Add(new TaskViewModel(new Task { Name = "task1", Description = "description1" }, new Messenger.Messenger()));
            taskViewModel.Add(new TaskViewModel(new Task { Name = "task2", Description = "description2" }, new Messenger.Messenger()));
            Tasks = taskViewModel;
        }

        #region Implementation of IMainViewModel

        public ObservableCollection<ITaskViewModel> Tasks { get; private set; }

        public int InCompleteTasks { get; private set; }

        public DateTime SetAlarmOff { get; set; }

        #endregion Implementation of IMainViewModel
    }
}