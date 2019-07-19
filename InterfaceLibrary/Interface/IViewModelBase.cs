using System;
using Messenger;

namespace InterfaceLibrary.Interface
{
    public interface IViewModelBase
    {
        Type Type { get; }

        IMessenger WindowMessenger { get; set; }
    }
}