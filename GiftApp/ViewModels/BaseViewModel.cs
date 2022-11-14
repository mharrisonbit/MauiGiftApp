using System;
using System.Collections.Specialized;

namespace GiftApp.ViewModels
{
    public class BaseViewModel : ViewModel, INotifyCollectionChanged
    {

        public BaseViewModel(BaseServices services) : base(services)
        {
        }

        [Reactive] public bool IsEnabled { get; set; }
        
        protected void SetBusyState(bool isBusy)
        {
            IsBusy = isBusy;
            IsEnabled = !isBusy;
        }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
    }
}

