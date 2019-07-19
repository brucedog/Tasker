using System;
using Messenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Models;
using MorningReminder.Commands;
using MorningReminder.ViewModel;

namespace ViewModelUnitTests
{
    [TestClass]
    public class AddTaskViewModelTests
    {
        [TestMethod]
        public void Create_add_task_view_model_with_empty_task_model()
        {
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel(new Messenger.Messenger());

            Assert.AreEqual(null, addTaskViewModel.TaskName);
            Assert.AreEqual(null, addTaskViewModel.TaskDescription);
        }

        [TestMethod]
        public void Create_add_tasK_command()
        {
            AddTaskViewModel taskViewModel = new AddTaskViewModel(new Messenger.Messenger());

            Assert.IsInstanceOfType(taskViewModel.AddTaskCommand, typeof(RelayCommand));
        }

        [TestMethod]
        public void Create_close_command()
        {
            AddTaskViewModel mainViewModel = new AddTaskViewModel(new Messenger.Messenger());

            Assert.IsInstanceOfType(mainViewModel.CloseCommand, typeof(RelayCommand));
        }

        [TestMethod]
        public void Create_add_task_view_model_can_set_properties()
        {
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel(new Messenger.Messenger());
            addTaskViewModel.TaskName = "Name";
            addTaskViewModel.TaskDescription = "Description";

            Assert.AreEqual("Name", addTaskViewModel.TaskName);
            Assert.AreEqual("Description", addTaskViewModel.TaskDescription);
        }

        [TestMethod]
        public void Excute_add_task_notify_update()
        {
            bool notified = false;
            //// Register an action to set the variables that are used to verify the command notified the messenger correctly
            Action<ITask> action = q =>
            {
                notified = true;
            };

            IMessenger messenger = new Messenger.Messenger();
            messenger.RegisterAction(Messages.ADD_TASK, action);
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel(messenger);

            addTaskViewModel.AddTaskCommand.Execute(null);

            // Check parameters passed to messenger
            Assert.IsTrue(notified);
        }

        [TestMethod]
        public void Excute_add_task_clears_input()
        {
            bool notified = false;
            //// Register an action to set the variables that are used to verify the command notified the messenger correctly
            Action<ITask> action = q =>
            {
                notified = true;
            };

            IMessenger messenger = new Messenger.Messenger();
            messenger.RegisterAction(Messages.ADD_TASK, action);
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel(messenger) { TaskName = "name", TaskDescription = "description" };

            addTaskViewModel.AddTaskCommand.Execute(null);

            // Check parameters passed to messenger
            Assert.IsTrue(notified);
            Assert.AreEqual(string.Empty, addTaskViewModel.TaskName);
            Assert.AreEqual(string.Empty, addTaskViewModel.TaskDescription);
        }

        [TestMethod]
        public void Excute_close_add_task_window_notify_to_close_it()
        {
            bool notified = false;
            //// Register an action to set the variables that are used to verify the command notified the messenger correctly
            Action action = () => { notified = true; };

            IMessenger messenger = new Messenger.Messenger();
            messenger.RegisterAction(Messages.CLOSE_ADD_TASK_WINDOW, action);
            AddTaskViewModel addTaskViewModel = new AddTaskViewModel(messenger);

            addTaskViewModel.CloseCommand.Execute(null);

            // Check parameters passed to messenger
            Assert.IsTrue(notified);
        }
    }
}