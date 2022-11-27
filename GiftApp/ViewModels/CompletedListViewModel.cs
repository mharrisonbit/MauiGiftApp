using System;
using GiftApp.Models;

namespace GiftApp.ViewModels;

public class CompletedListViewModel : BaseViewModel
{
    private readonly BaseServices services;
    private readonly ISqliteConnection sqliteConnection;

    public ICommand GiftPurchasedButton { get; }

    public CompletedListViewModel(BaseServices services, ISqliteConnection sqliteConnection) : base(services)
    {
        this.services = services;
        this.sqliteConnection = sqliteConnection;

        this.GiftPurchasedButton = new Command<Gift>((x) => this.MarkGiftCommand(x));
    }

    [Reactive] public ObservableCollection<Models.Person> People { get; set; }

    private void MarkGiftCommand(Gift x)
    {
        this.sqliteConnection.UpdateGift(x);
        People = this.sqliteConnection.GetAllPeople(true);
    }

    public override void OnNavigatedTo(INavigationParameters parameters)
    {
        People = this.sqliteConnection.GetAllPeople(true);
    }
}