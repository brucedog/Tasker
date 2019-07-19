using System.Windows;
using Model.Models;

namespace InterfaceLibrary.Interface
{
    public interface ITaskViewModel
    {
        string TaskName { get; set; }

        string TaskDescription { get; set; }

        ITask Task { get; }

        Visibility Visibility { get; set; }
    }
}