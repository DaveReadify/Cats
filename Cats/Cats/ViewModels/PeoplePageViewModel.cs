using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Cats.ViewModels
{
    public interface IPeoplePageViewModel
    {
        ObservableCollection<Pet> OwnedByMale { get; set; }
        ObservableCollection<Pet> OwnedByFemale { get; set; }
        IList<Person> People { get; set; }

        bool IsSearching { get; set; }

        RelayCommand GetPeopleCommand { get; }
    }
    
    public class PeoplePageViewModel : ViewModelBase, IPeoplePageViewModel
    {
        private readonly IJsonService _jsonService;
        private RelayCommand _loginCommand;
        private bool _isSearching;

        public PeoplePageViewModel(IJsonService jsonService)
        {
            _jsonService = jsonService;

            OwnedByMale = new ObservableCollection<Pet>();
            OwnedByFemale = new ObservableCollection<Pet>();
        }

        public ObservableCollection<Pet> OwnedByMale { get; set; }
        public ObservableCollection<Pet> OwnedByFemale { get; set; }
        public IList<Person> People { get; set; }

        public bool IsSearching
        {
            get { return _isSearching; }
            set { Set(ref _isSearching, value); }
        }

        public RelayCommand GetPeopleCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new RelayCommand(async () => await PerformQueryAsync()));
            }
        }

        private async Task PerformQueryAsync()
        {
            try
            {
                IsSearching = true;

                OwnedByMale.Clear();
                OwnedByFemale.Clear();

                var people = await _jsonService.GetPeople();

                AssignCatsToGender(people);
            }
            catch (Exception ex)
            {
                // log it and show a friendly error message
            }
            finally
            {
                IsSearching = false;
            }
        }

        private void AssignCatsToGender(List<Person> people)
        {
            var femaleOwned = new List<Pet>();
            var maleOwned = new List<Pet>();

            foreach (var person in people.Where(p => p.Pets != null && p.PersonGender == Gender.Male))
            {
                maleOwned.AddRange(person.Pets.Where(p => p.PetType == PetType.Cat));
            }

            foreach (var person in people.Where(p => p.Pets != null && p.PersonGender == Gender.Female))
            {
                femaleOwned.AddRange(person.Pets.Where(p => p.PetType == PetType.Cat));
            }

            foreach (var pet in maleOwned.OrderBy(p => p.Name))
            {
                OwnedByMale.Add(pet);
            }

            foreach (var pet in femaleOwned.OrderBy(p => p.Name))
            {
                OwnedByFemale.Add(pet);
            }
        }
    }
}
