using System;
using GiftApp.Interfaces;
using GiftApp.Models;

namespace GiftApp.ViewModels
{
    public class AddToListViewModel : BaseViewModel
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

        private Person _personToAdd;
        public Person PersonToAdd
        {
            get => _personToAdd;
            set => this.RaiseAndSetIfChanged(ref _personToAdd, value);
        }

        void AddPerson()
        {
            IsBusy = true;
            if (!string.IsNullOrWhiteSpace(PersonToAdd.BirthdateText))
            {
                var fixedBirthDate = DateTime.Parse(PersonToAdd.BirthdateText);
                PersonToAdd.Birthday = fixedBirthDate;
                PersonToAdd.Age = DateTime.Today.Year - PersonToAdd.Birthday.Year;
            }
            var answer = this.sqlConnection.AddPerson(PersonToAdd);
            PersonToAdd = new();
            IsBusy = false;
        }
    }
}

