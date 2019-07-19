using System.Collections.ObjectModel;

namespace InterfaceLibrary.Interface
{
    public interface IMainViewModel
    {
        ObservableCollection<ITaskViewModel> Tasks { get; }        
    }
}