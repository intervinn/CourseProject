using CourseProject.Helpers;
using CourseProject.Models;
using CourseProject.Views.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace CourseProject.ViewModels.Pages
{
    public partial class CompanyViewModel : ObservableObject, IModelForm
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;

        public CompanyViewModel(AppDbContext dbContext, INavigationWindow navigationWindow)
        {
            _navigationWindow = navigationWindow;
            _dbContext = dbContext;
        }

        [ObservableProperty]
        private string _name;
        [ObservableProperty]
        private string _country;

        public string Mode = "Add";
        public Company Archetype;

        public void Load(object archetype)
        {
            if (archetype is Company comp)
            {
                Archetype = comp;
                Name = comp.Name;
                Country = comp.Country;
            }
        }

        public void Reset()
        {
            Name = "";
            Country = "";
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
                _dbContext.Companies.Add(new Company()
                {
                    Name = Name,
                    Country = Country
                });
            }

            if (Mode == "Edit")
            {
                Archetype.Name = Name;
                Archetype.Country = Country;
            }

            _dbContext.SaveChanges();
            _navigationWindow.Navigate(typeof(EditorPage));
        }
    }
}
