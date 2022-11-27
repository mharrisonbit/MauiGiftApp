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
        public ICommand DeleteGiftButton { get; }
        public ICommand GiftPurchasedButton { get; }
        public ICommand CompletedListButton { get; }

        public HomeViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            this.navigator = navigator;
            this.sqliteConnection = sqliteConnection;
            this.DeleteAllDataCommand = new Command(() => this.DeleteAllData());
            this.AddGiftButton = new Command<int>((x) => this.AddGiftCommand(x));
            this.DeleteGiftButton = new Command<Gift>((x) => this.DeleteGiftCommand(x));
            this.GiftPurchasedButton = new Command<Gift>((x) => this.MarkGiftCommand(x));
            this.CompletedListButton = new Command(() => this.ShowCompletedListCommand());

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate(uri);
            });
        }

        [Reactive] public string Property { get; set; }
        [Reactive] public string Title { get; set; } = "Home Page From VM";
        [Reactive] public ObservableCollection<Person> People { get; set; }

        private void DeleteAllData()
        {
            People = this.sqliteConnection.DropAllTables();
        }

        private void GetListOfPeopleToDisplay()
        {
            People = this.sqliteConnection.GetAllPeople(false);
        }

        private void AddGiftCommand(int id)
        {
            Console.WriteLine(id);
            var navParameters = new NavigationParameters
            {
                { "RecieverId", id }
            };
            
            this.navigator.Navigate("AddGiftView", navParameters);
        }

        private void DeleteGiftCommand(Gift gift)
        {
            var test = this.sqliteConnection.DeleteGiftFromUser(gift);
            if (test)
                GetListOfPeopleToDisplay();
        }

        private void MarkGiftCommand(Gift x)
        {
            this.sqliteConnection.UpdateGift(x);
            People = this.sqliteConnection.GetAllPeople(false);
        }

        private void UpdateAmountToSpendShown()
        {

        }

        private void ShowCompletedListCommand()
        {
            this.navigator.Navigate("CompletedListView");
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            GetListOfPeopleToDisplay();
        }
    }
}