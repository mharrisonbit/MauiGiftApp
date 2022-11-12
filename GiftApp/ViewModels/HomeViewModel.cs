using System;

namespace GiftApp.ViewModels
{
	public class HomeViewModel : ViewModel
	{
        public ICommand Navigate { get; }

		public HomeViewModel(BaseServices services, INavigationService navigator) : base(services)
        {
            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });
        }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";
    }
}

