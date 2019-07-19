using Messenger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MorningReminder.ViewModel;

namespace ViewModelUnitTests
{
    [TestClass]
    public class ViewModelBaseTests
    {
        [TestMethod]
        public void View_model_fires_notify_property_changed()
        {
            ImplementedViewModelBase sut = new ImplementedViewModelBase();

            int called = 0;
            sut.PropertyChanged += (sender, e) =>
            {
                called++;
            };

            sut.TestProperty = "Changed!";

            Assert.AreEqual("Changed!", sut.TestProperty);
            Assert.AreEqual(1, called);
        }

        public class ImplementedViewModelBase : ViewModelBase
        {
            private string _testProperty;

            public ImplementedViewModelBase()
            {
            }

            public ImplementedViewModelBase(IMessenger messenger)
            {
            }

            public string TestProperty
            {
                get
                {
                    return _testProperty;
                }

                set
                {
                    _testProperty = value;
                    NotifyPropertyChanged(() => TestProperty);
                }
            }
        }
    }
}