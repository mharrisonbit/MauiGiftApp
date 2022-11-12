using System;

namespace GiftApp.ViewModels
{
    public class AddToListViewModel : ViewModel
    {
        public ICommand Navigate { get; }

        public AddToListViewModel(BaseServices services) : base(services)
        {
            
        }
    }
}

