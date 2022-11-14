using System;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class HomeViewModel : BaseViewModel, INavigationAware
    {
        private readonly INavigationService navigator;
        private readonly ISqliteConnection sqliteConnection;

        public ICommand Navigate { get; }
        public ICommand DeleteAllDataCommand { get; }
        public ICommand AddGiftButton { get; }

        public HomeViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            this.navigator = navigator;
            this.sqliteConnection = sqliteConnection;
            this.DeleteAllDataCommand = new Command(() => this.DeleteAllData());
            this.AddGiftButton = new Command<int>((x) => this.AddGiftCommand(x));

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });

            //People = this.sqliteConnection.GetAllPeople();
        }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";
        [Reactive] public ObservableCollection<Person> People { get; set; }

        private void DeleteAllData()
        {
            this.sqliteConnection.DropAllTables();
        }

        private void GetListOfPeopleToDisplay()
        {
            var peopleList = this.sqliteConnection.GetAllPeople();
            People = new ObservableCollection<Person>(peopleList);
        }

        private void AddGiftCommand(int id)
        {
            Console.WriteLine(id);
            var navParameters = new NavigationParameters
            {
                { "RecieverId", id }
            };
            
            this.navigator.Navigate("NavigationPage/AddGiftView", navParameters);
        }


        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetListOfPeopleToDisplay();
        }
    }
}

