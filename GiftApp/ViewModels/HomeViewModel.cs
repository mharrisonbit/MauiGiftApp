using System;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class HomeViewModel : BaseViewModel, INavigationAware
    {
        private readonly ISqliteConnection sqliteConnection;

        public ICommand Navigate { get; }
        public ICommand DeleteAllDataCommand { get; }

        public HomeViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            this.sqliteConnection = sqliteConnection;
            this.DeleteAllDataCommand = new Command(() => this.DeleteAllData());

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });

            //People = this.sqliteConnection.GetAllPeople();
        }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";

        //public ObservableCollection<Person> People { get; private set; } = new();

        private ObservableCollection<Person> _people;
        public ObservableCollection<Person> People
        {
            get => _people;
            set => this.RaiseAndSetIfChanged(ref _people, value);
        }

        private void DeleteAllData()
        {
            this.sqliteConnection.DropAllTables();
        }

        private void GetListOfPeopleToDisplay()
        {
            var peopleList = this.sqliteConnection.GetAllPeople();
            People = new ObservableCollection<Person>(peopleList);
            //foreach (var person in People)
            //{
            //    Console.WriteLine(person.FirstName);
            //}
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetListOfPeopleToDisplay();
        }
    }
}

