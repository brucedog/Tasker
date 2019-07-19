using System.Windows;
using Model.Models;

namespace InterfaceLibrary.Interface
{
    public interface ITaskViewModel
    {
        string TaskName { get; }

        string TaskDescription { get; }

        ITask Task { get; }
        
        Visibility Visibility { get; set; }
    }
}