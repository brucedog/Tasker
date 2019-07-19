using System.Collections.ObjectModel;
using InterfaceLibrary.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using MorningReminder.Commands;
using MorningReminder.ViewModel;

namespace ViewModelUnitTests
{
    [TestClass]
    public class MainViewTests
    {
        private ObservableCollection<ITaskViewModel> taskVModels = new ObservableCollection<ITaskViewModel>();
        private ITask task = new Task { Name = "Task", Description = "description" };
        private ITaskViewModel taskViewModel = new TaskViewModel(new Task { Name = "Task", Description = "description" }, new Messenger.Messenger());

        [TestMethod]
        public void Create_add_task_command()
        {
            MainViewModel mainViewModel = new MainViewModel(new Messenger.Messenger());

            Assert.IsInstanceOfType(mainViewModel.AddTaskCommand, typeof(RelayCommand));
        }
    }
}