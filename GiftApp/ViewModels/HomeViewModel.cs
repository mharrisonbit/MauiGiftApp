using System;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class HomeViewModel : ViewModel
    {
        private readonly ISqliteConnection sqliteConnection;

        public ICommand Navigate { get; }

        public HomeViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            this.sqliteConnection = sqliteConnection;

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });

            People = this.sqliteConnection.GetAllPeople();
        }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";

        public ObservableCollection<Person> People { get; private set; } = new();

    }
}

