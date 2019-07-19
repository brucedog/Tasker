using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using MorningReminder.Commands;
using MorningReminder.ViewModel;

namespace ViewModelUnitTests
{
    [TestClass]
    public class TaskViewModelTests
    {
        [TestMethod]
        public void Task_view_model_properties_of_param()
        {
            ITask task = new Task { Name = "TaskName", Description = "Task Description" };
            TaskViewModel taskViewModel = new TaskViewModel(task, new Messenger.Messenger());

            Assert.AreEqual(task.Name, taskViewModel.TaskName);
            Assert.AreEqual(task.Description, taskViewModel.TaskDescription);
        }

        [TestMethod]
        public void Create_delete_command()
        {
            TaskViewModel taskViewModel = new TaskViewModel(new Task(), new Messenger.Messenger());

            Assert.IsInstanceOfType(taskViewModel.DeleteTaskCommand, typeof(RelayCommand));
        }

        [Ignore]
        public void Delete_command_removes_tasks()
        {
            //MainViewModel mainViewModel = new MainViewModel();

            //taskVModels.Add(taskViewModel);
            //Type type = typeof(MainViewModel);
            //FieldInfo taskViewModelField = type.GetField("_taskViewModels", BindingFlags.NonPublic | BindingFlags.Instance);
            //FieldInfo taskListField = type.GetField("_taskList", BindingFlags.NonPublic | BindingFlags.Instance);
            //taskListField.SetValue(mainViewModel, new List<ITask> { task });
            //taskViewModelField.SetValue(mainViewModel, taskVModels);

            //int orginalTaskCount = mainViewModel.Tasks.Count;
            //mainViewModel.DeleteTaskCommand.Execute(taskVModels);

            //Assert.AreEqual(0, mainViewModel.Tasks.Count);
            //Assert.AreNotEqual(orginalTaskCount, mainViewModel.Tasks.Count);
        }
    }
}