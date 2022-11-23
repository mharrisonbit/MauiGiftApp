using System;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class AddGiftViewModel : BaseViewModel
    {
        private readonly ISqliteConnection conn;
        private int recieverId;

        public ICommand AddGiftButton { get; }

        [Reactive] public Gift GiftToAdd { get; set; } = new();
        [Reactive] public Person PersonToBuyFor { get; set; }

        public AddGiftViewModel(BaseServices services, ISqliteConnection conn) : base(services)
        {
            this.conn = conn;
            this.AddGiftButton = new Command(() => this.AddGiftCommand());
        }

        private void AddGiftCommand()
        {
            SetBusyState(true);
            _= this.conn.AddGiftForUser(GiftToAdd);
            SetBusyState(false);
            this.Navigation.GoBack();
        }
        private void GetPersonToBuyFor(int recieverId)
        {
            PersonToBuyFor = this.conn.GetPersonById(recieverId);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            recieverId = parameters.GetValue<int>("RecieverId");
            GetPersonToBuyFor(recieverId);
            GiftToAdd.PersonId = recieverId;
        }
    }
}