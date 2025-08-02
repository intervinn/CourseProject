using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CourseProject.Helpers;
using CourseProject.Models;
using CourseProject.Views.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using Wpf.Ui;

namespace CourseProject.ViewModels.Pages
{
    public partial class MotorcycleViewModel : ObservableObject, IModelForm
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;

        public MotorcycleViewModel(AppDbContext dbContext, INavigationWindow navigationWindow)
        {
            _navigationWindow = navigationWindow;
            _dbContext = dbContext;

            Persons = _dbContext.People.ToList();
            Companies = _dbContext.Companies.ToList();
            TiresList = _dbContext.Tires.ToList();
            HeadlightsList = _dbContext.Headlights.ToList();

            ComfortFeatures = [.. Enum.GetValues<ComfortFeature>().Select(f => new ComfortFeatureItem(f))];
        }

        [ObservableProperty]
        private string _model = string.Empty;

        [ObservableProperty]
        private string _country = string.Empty;

        [ObservableProperty]
        private MotorcycleType _type;

        [ObservableProperty]
        private string _color = string.Empty;

        [ObservableProperty]
        private int _productionYear = DateTime.Now.Year;

        [ObservableProperty]
        private int _maxSpeed;

        [ObservableProperty]
        private string _registrationNumber = string.Empty;

        [ObservableProperty]
        private DateTime _lastInspectionDate = DateTime.Now;

        [ObservableProperty]
        private Person _selectedOwner;

        [ObservableProperty]
        private Company _selectedManufacturer;

        [ObservableProperty]
        private Tires _selectedTires;

        [ObservableProperty]
        private Headlights _selectedHeadlights;

        public ObservableCollection<ComfortFeatureItem> ComfortFeatures { get; }
        public List<Person> Persons { get; set; }
        public List<Company> Companies { get; set; }
        public List<Tires> TiresList { get; set; }
        public List<Headlights> HeadlightsList { get; set; }
        public List<MotorcycleType> MotorcycleTypes => Enum.GetValues<MotorcycleType>().ToList();

        public string Mode = "Add";
        public Motorcycle Archetype;

        [RelayCommand]
        private void OnConfirm()
        {
            var comfortFeatures = ComfortFeatures
                .Where(c => c.IsSelected)
                .Select(c => c.Feature)
                .ToList();

            if (Mode == "Add")
            {
                _dbContext.Motorcycles.Add(new Motorcycle()
                {
                    Model = Model,
                    Country = Country,
                    Type = Type,
                    Color = Color,
                    ProductionYear = ProductionYear,
                    MaxSpeed = MaxSpeed,
                    RegistrationNumber = RegistrationNumber,
                    LastInspectionDate = LastInspectionDate,
                    ComfortFeatures = comfortFeatures,
                    OwnerId = SelectedOwner?.Id,
                    ManufacturerId = SelectedManufacturer?.Id,
                    TiresId = SelectedTires?.Id,
                    HeadlightsId = SelectedHeadlights?.Id
                });
            }

            if (Mode == "Edit")
            {
                Archetype.Model = Model;
                Archetype.Country = Country;
                Archetype.Type = Type;
                Archetype.Color = Color;
                Archetype.ProductionYear = ProductionYear;
                Archetype.MaxSpeed = MaxSpeed;
                Archetype.RegistrationNumber = RegistrationNumber;
                Archetype.LastInspectionDate = LastInspectionDate;
                Archetype.ComfortFeatures = comfortFeatures;
                Archetype.OwnerId = SelectedOwner?.Id;
                Archetype.ManufacturerId = SelectedManufacturer?.Id;
                Archetype.TiresId = SelectedTires?.Id;
                Archetype.HeadlightsId = SelectedHeadlights?.Id;
            }

            _dbContext.SaveChanges();
            _navigationWindow.Navigate(typeof(EditorPage));
        }

        public void Load(object archetype)
        {
            Persons = _dbContext.People.ToList();
            Companies = _dbContext.Companies.ToList();
            TiresList = _dbContext.Tires.ToList();
            HeadlightsList = _dbContext.Headlights.ToList();
            if (archetype is Motorcycle motorcycle)
            {
                Archetype = motorcycle;
                Model = motorcycle.Model;
                Country = motorcycle.Country;
                Type = motorcycle.Type;
                Color = motorcycle.Color;
                ProductionYear = motorcycle.ProductionYear;
                MaxSpeed = motorcycle.MaxSpeed;
                RegistrationNumber = motorcycle.RegistrationNumber;
                LastInspectionDate = motorcycle.LastInspectionDate;
                SelectedOwner = Persons.FirstOrDefault(p => p.Id == motorcycle.OwnerId);
                SelectedManufacturer = Companies.FirstOrDefault(c => c.Id == motorcycle.ManufacturerId);
                SelectedTires = TiresList.FirstOrDefault(t => t.Id == motorcycle.TiresId);
                SelectedHeadlights = HeadlightsList.FirstOrDefault(h => h.Id == motorcycle.HeadlightsId);

                // Reset all comfort features
                foreach (var item in ComfortFeatures)
                {
                    item.IsSelected = false;
                }

                // Select existing comfort features
                if (motorcycle.ComfortFeatures != null)
                {
                    foreach (var feature in motorcycle.ComfortFeatures)
                    {
                        var item = ComfortFeatures.FirstOrDefault(c => c.Feature == feature);
                        if (item != null) item.IsSelected = true;
                    }
                }
            }
        }

        public void Reset()
        {
            Persons = _dbContext.People.ToList();
            Companies = _dbContext.Companies.ToList();
            TiresList = _dbContext.Tires.ToList();
            HeadlightsList = _dbContext.Headlights.ToList();
            Model = string.Empty;
            Country = string.Empty;
            Type = default;
            Color = string.Empty;
            ProductionYear = DateTime.Now.Year;
            MaxSpeed = 0;
            RegistrationNumber = string.Empty;
            LastInspectionDate = DateTime.Now;
            SelectedOwner = null;
            SelectedManufacturer = null;
            SelectedTires = null;
            SelectedHeadlights = null;

            foreach (var item in ComfortFeatures)
            {
                item.IsSelected = false;
            }
        }

        public void SetMode(string mode)
        {
            Mode = mode;
        }

        public class ComfortFeatureItem
        {
            public ComfortFeature Feature { get; }
            public bool IsSelected { get; set; }
            public string DisplayName { get; }

            public ComfortFeatureItem(ComfortFeature feature)
            {
                Feature = feature;
                DisplayName = GetDisplayName(feature);
            }

            private string GetDisplayName(ComfortFeature feature)
            {
                var memberInfo = typeof(ComfortFeature).GetMember(feature.ToString());
                var attribute = memberInfo[0].GetCustomAttributes<LocalizedColumnAttribute>().FirstOrDefault();
                return attribute?.Name ?? feature.ToString();
            }
        }
    }
}