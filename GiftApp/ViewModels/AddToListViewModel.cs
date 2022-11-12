using System;
using GiftApp.Interfaces;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class AddToListViewModel : ViewModel
    {
        public ICommand Navigate { get; }
        public ICommand AddPersonCmd { get; }

        readonly ISqliteConnection sqlConnection;
        

        public AddToListViewModel(BaseServices services, INavigationService navigator, ISqliteConnection sqliteConnection) : base(services)
        {
            sqlConnection = sqliteConnection;

            this.Navigate = ReactiveCommand.CreateFromTask<string>(async uri =>
            {
                await navigator.Navigate("NavigationPage/" + uri);
            });

            this.AddPersonCmd = new Command(() => this.AddPerson());

            PersonToAdd = new Person();
        }

        Person _personToAdd;
        public Person PersonToAdd
        {
            get => _personToAdd;
            private set => _personToAdd = value;
        }

        void AddPerson()
        {
            var answer = this.sqlConnection.AddPerson(PersonToAdd);
        }
    }
}

