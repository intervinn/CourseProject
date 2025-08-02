using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.Helpers;
using CourseProject.Models;
using CourseProject.Views.Pages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Wpf.Ui;

namespace CourseProject.ViewModels.Pages
{
    public partial class PersonViewModel : ObservableObject, IModelForm
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;

        public PersonViewModel(AppDbContext dbContext, INavigationWindow navigationWindow)
        {
            _navigationWindow = navigationWindow;
            _dbContext = dbContext;
        }

        [ObservableProperty]
        private string _firstName = string.Empty;

        [ObservableProperty]
        private string _lastName = string.Empty;

        [ObservableProperty]
        private DateTime _birthDate = DateTime.Now.AddYears(-18);

        [ObservableProperty]
        private string _licenseNumber = string.Empty;

        [ObservableProperty]
        private string _contactPhone = string.Empty;

        [ObservableProperty]
        private string _address = string.Empty;

        public string Mode = "Add";
        public Person Archetype;

        public void Load(object archetype)
        {
            if (archetype is Person person)
            {
                Archetype = person;
                FirstName = person.FirstName;
                LastName = person.LastName;
                BirthDate = person.BirthDate;
                LicenseNumber = person.LicenseNumber;
                ContactPhone = person.ContactPhone;
                Address = person.Address;
            }
        }

        public void Reset()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            BirthDate = DateTime.Now.AddYears(-18);
            LicenseNumber = string.Empty;
            ContactPhone = string.Empty;
            Address = string.Empty;
        }

        public void SetMode(string mode)
        {
            Mode = mode;
        }

        [RelayCommand]
        private void OnConfirm()
        {
            if (Mode == "Add")
            {
                _dbContext.People.Add(new Person()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    BirthDate = BirthDate,
                    LicenseNumber = LicenseNumber,
                    ContactPhone = ContactPhone,
                    Address = Address
                });
            }

            if (Mode == "Edit")
            {
                Archetype.FirstName = FirstName;
                Archetype.LastName = LastName;
                Archetype.BirthDate = BirthDate;
                Archetype.LicenseNumber = LicenseNumber;
                Archetype.ContactPhone = ContactPhone;
                Archetype.Address = Address;
            }

            _dbContext.SaveChanges();
            _navigationWindow.Navigate(typeof(EditorPage));
        }
    }
}