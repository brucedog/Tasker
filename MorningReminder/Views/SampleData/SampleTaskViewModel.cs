using System.Windows;
using InterfaceLibrary.Interface;
using Model.Models;

namespace MorningReminder.Views.SampleData
{
    public class SampleTaskViewModel : ITaskViewModel
    {
        public SampleTaskViewModel()
        {
            Task = new Task { Name = "Task Name", Description = "Task Description" };
            TaskName = "Task Name";
            TaskDescription = "Task Description";
        }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public bool IsTaskCompleted { get; set; }

        public ITask Task { get; private set; }

        public Visibility Visibility { get; set; }
    }
}