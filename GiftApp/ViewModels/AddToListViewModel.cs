using System;
using GiftApp.Interfaces;

namespace GiftApp.ViewModels
{
    public class AddToListViewModel : ViewModel
    {
        public ICommand Navigate { get; }
        readonly ISqliteConnection sqlConnection;

        public AddToListViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            sqlConnection = sqliteConnection;

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });

            this.sqlConnection.DoSomething();
        }
    }
}

