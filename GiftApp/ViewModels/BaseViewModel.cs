using System;
using System.Collections.Specialized;

namespace GiftApp.ViewModels
{
    public class BaseViewModel : ViewModel, INotifyCollectionChanged
    {

        public BaseViewModel(BaseServices services) : base(services)
        {
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}

