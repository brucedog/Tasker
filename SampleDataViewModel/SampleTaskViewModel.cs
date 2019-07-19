using Model.Models;
using MorningReminder.Interface;

namespace SampleDataViewModel
{
    public class SampleTaskViewModel : ITaskViewModel
    {
        public void SampleDataViewModel()
        {
            Task = new Task { Name = "Task Name", Description = "Task Description" };
            TaskName = "Task Name";
            TaskDescription = "Task Description";
        }

        public string TaskName { get; private set; }

        public string TaskDescription { get; private set; }
        
        public bool IsTaskCompleted { get; set; }
        
        public ITask Task { get; private set; }
    }
}