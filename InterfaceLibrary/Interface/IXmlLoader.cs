using System.Collections.Generic;
using Model.Models;

namespace InterfaceLibrary.Interface
{
    public interface IXmlLoader
    {
        IEnumerable<ITask> LoadTasksFromXmlFile();

        void SaveXmlFile(IList<ITask> tasks);
    }
}