using System;

namespace GiftApp.ViewModels
{
	public class HomeViewModel : ViewModel
	{
		public HomeViewModel(BaseServices services) : base(services) { }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";
    }
}

