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
    public partial class HeadlightsViewModel : ObservableObject, IModelForm
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;

        public HeadlightsViewModel(AppDbContext dbContext, INavigationWindow navigationWindow)
        {
            _navigationWindow = navigationWindow;
            _dbContext = dbContext;
            Companies = _dbContext.Companies.ToList();
        }

        [ObservableProperty]
        private HeadlightType _type;

        [ObservableProperty]
        private bool _isAutomatic;

        [ObservableProperty]
        private bool _hasAdaptiveCornering;

        [ObservableProperty]
        private int _lumenOutput;

        [ObservableProperty]
        private Company? _selectedManufacturer;

        public List<Company> Companies { get; set; }
        public List<HeadlightType> HeadlightTypes => Enum.GetValues<HeadlightType>().ToList();

        public string Mode = "Add";
        public Headlights Archetype;

        public void Reset()
        {
            Companies = _dbContext.Companies.ToList();
            Type = default;
            IsAutomatic = false;
            HasAdaptiveCornering = false;
            LumenOutput = 0;
            SelectedManufacturer = null;
        }

        public void Load(object archetype)
        {
            Companies = _dbContext.Companies.ToList();
            if (archetype is Headlights headlights)
            {
                Archetype = headlights;
                Type = headlights.Type;
                IsAutomatic = headlights.IsAutomatic;
                HasAdaptiveCornering = headlights.HasAdaptiveCornering;
                LumenOutput = headlights.LumenOutput;
                SelectedManufacturer = Companies.FirstOrDefault(c => c.Id == headlights.ManufacturerId);
            }
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
                _dbContext.Headlights.Add(new Headlights()
                {
                    Type = Type,
                    IsAutomatic = IsAutomatic,
                    HasAdaptiveCornering = HasAdaptiveCornering,
                    LumenOutput = LumenOutput,
                    ManufacturerId = SelectedManufacturer?.Id
                });
            }

            if (Mode == "Edit")
            {
                Archetype.Type = Type;
                Archetype.IsAutomatic = IsAutomatic;
                Archetype.HasAdaptiveCornering = HasAdaptiveCornering;
                Archetype.LumenOutput = LumenOutput;
                Archetype.ManufacturerId = SelectedManufacturer?.Id;
            }

            _dbContext.SaveChanges();
            _navigationWindow.Navigate(typeof(EditorPage));
        }
    }
}