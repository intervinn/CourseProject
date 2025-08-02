using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.Helpers;
using CourseProject.Models;
using CourseProject.Views.Pages;
using System.Collections.Generic;
using System.Linq;
using Wpf.Ui;

namespace CourseProject.ViewModels.Pages
{
    public partial class TiresViewModel : ObservableObject, IModelForm
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;

        public TiresViewModel(AppDbContext dbContext, INavigationWindow navigationWindow)
        {
            _navigationWindow = navigationWindow;
            _dbContext = dbContext;
            Companies = _dbContext.Companies.ToList();
        }

        [ObservableProperty]
        private string _model = string.Empty;

        [ObservableProperty]
        private string _size = string.Empty;

        [ObservableProperty]
        private string _type = string.Empty;

        [ObservableProperty]
        private Company _selectedManufacturer;

        public List<Company> Companies { get; set; }
        public string Mode = "Add";
        public Tires Archetype;

        public void Load(object archetype)
        {
            Companies = _dbContext.Companies.ToList();
            if (archetype is Tires tires)
            {
                Archetype = tires;
                Model = tires.Model;
                Size = tires.Size;
                Type = tires.Type;
                SelectedManufacturer = Companies.FirstOrDefault(c => c.Id == tires.ManufacturerId);
            }
        }

        public void Reset()
        {
            Companies = _dbContext.Companies.ToList();
            Model = string.Empty;
            Size = string.Empty;
            Type = string.Empty;
            SelectedManufacturer = null;
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
                _dbContext.Tires.Add(new Tires()
                {
                    Model = Model,
                    Size = Size,
                    Type = Type,
                    ManufacturerId = SelectedManufacturer?.Id
                });
            }

            if (Mode == "Edit")
            {
                Archetype.Model = Model;
                Archetype.Size = Size;
                Archetype.Type = Type;
                Archetype.ManufacturerId = SelectedManufacturer?.Id;
            }

            _dbContext.SaveChanges();
            _navigationWindow.Navigate(typeof(EditorPage));
        }
    }
}