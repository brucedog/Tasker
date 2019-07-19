using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using InterfaceLibrary.Interface;
using Model.Models;

namespace Utils.Utilities
{
    public class XmlLoader : IXmlLoader
    {
        private readonly string _target = Directory.GetCurrentDirectory() + "\\TaskXmlFile.xml";
        private const string SingleTask = "task";
        private const string MainTasks = "Tasks";
        private const string TaskName = "Name";
        private const string TaskDescription = "Description";
        private static IXmlLoader _xmlLoader;
        private static readonly object SyncRoot = new object();

        private XmlLoader()
        {
        }

        public static IXmlLoader Get
        {
            get
            {
                if (_xmlLoader == null)
                {
                    lock (SyncRoot)
                        return _xmlLoader = new XmlLoader();
                }

                return _xmlLoader;
            }
        }

        public IEnumerable<ITask> LoadTasksFromXmlFile()
        {
            if (!DoesXmlFileExist())
            {
                return new List<ITask>();
            }

            using (XmlReader xmlTextReader = new XmlTextReader(_target))
            {
                return (from tasks in XDocument.Load(xmlTextReader).Element(MainTasks).Elements(SingleTask)
                        select new Task
                                   {
                                       Name = tasks.Element(TaskName).Value,
                                       Description = tasks.Element(TaskDescription).Value
                                   }).ToList();
            }
        }

        public void SaveXmlFile(IList<ITask> tasks)
        {
            if (DoesXmlFileExist())
            {
                File.Delete(_target);
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(_target))
            {
                xmlWriter.WriteStartElement(MainTasks);

                foreach (ITask task in tasks)
                {
                    xmlWriter.WriteStartElement(SingleTask);

                    xmlWriter.WriteElementString(TaskName, task.Name);
                    xmlWriter.WriteElementString(TaskDescription, task.Description);

                    xmlWriter.WriteEndElement();
                }

                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }
        }

        private bool DoesXmlFileExist()
        {
            if (File.Exists(_target))
            {
                return true;
            }

            using (XmlWriter xmlWriter = XmlWriter.Create(_target))
            {
                xmlWriter.WriteStartElement(MainTasks);
                xmlWriter.WriteStartElement(SingleTask);
                xmlWriter.WriteElementString(TaskName, string.Empty);
                xmlWriter.WriteElementString(TaskDescription, string.Empty);
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndDocument();
            }

            return false;
        }
    }
}