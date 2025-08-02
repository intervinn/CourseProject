
using CourseProject.Helpers;
using CourseProject.Models;
using CourseProject.Views.Pages;
using CourseProject.Views.Windows;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Data;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace CourseProject.ViewModels.Pages
{
    public partial class EditorViewModel : ObservableObject
    {
        private AppDbContext _dbContext;
        private INavigationWindow _navigationWindow;
        private CompanyViewModel _companyViewModel;
        MotorcycleViewModel _motorcycleViewModel;
        HeadlightsViewModel _headlightsViewModel;
        PersonViewModel _personViewModel;
        TiresViewModel _tiresViewModel;

        public AppDbContext DbContext { get => _dbContext; }

        public List<Type> TableTypes =>
                [
                    typeof(Motorcycle),
                    typeof(Person),
                    typeof(Tires),
                    typeof(Company),
                    typeof(Headlights),
                ];

        
        public ObservableCollection<string> Tables
        {
            get
            {
                ObservableCollection<string> result = [];
                foreach (var t in TableTypes)
                {
                    var localized = t.GetCustomAttribute(typeof(LocalizedColumnAttribute)) as LocalizedColumnAttribute;
                    if (localized is not null)
                    {
                        result.Add(localized.Name);
                    }
                }
                return result;
            }
        }

        [ObservableProperty]
        private string _currentTable;
        

        public Wpf.Ui.Controls.GridView Grid { get; set; }
        public Wpf.Ui.Controls.ListView List { get; set; }

        public EditorViewModel(
            AppDbContext dbContext,
            INavigationWindow navigationWindow,
            CompanyViewModel companyViewModel,
            MotorcycleViewModel motorcycleViewModel,
            HeadlightsViewModel headlightsViewModel,
            PersonViewModel personViewModel,
            TiresViewModel tiresViewModel
            )
        {
            _companyViewModel = companyViewModel;
            _motorcycleViewModel = motorcycleViewModel;
            _headlightsViewModel = headlightsViewModel;
            _personViewModel = personViewModel;
            _tiresViewModel = tiresViewModel;
            
            _navigationWindow = navigationWindow;
            
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Load column names
        /// </summary>
        /// <param name="t"></param>
        public void LoadColumns(Type t)
        {
            Grid.Columns.Clear();

            foreach (var p in t.GetProperties())
            {
                var localized = p.GetCustomAttribute(typeof(LocalizedColumnAttribute)) as LocalizedColumnAttribute;
                if (localized != null)
                {
                    Grid.Columns.Add(new Wpf.Ui.Controls.GridViewColumn()
                    {
                        Header = localized.Name,
                        DisplayMemberBinding = new Binding(p.Name),
                    });
                    
                }
            }
        }

        /// <summary>
        /// Load grid data
        /// </summary>
        /// <param name="table"></param>
        public void LoadData(string table)
        {
            IEnumerable<object> data = table switch
            {
                "Мотоцикл" => _dbContext.Motorcycles.ToList(),
                "Шины" => _dbContext.Tires.ToList(),
                "Фары" => _dbContext.Headlights.ToList(),
                "Человек" => _dbContext.People.ToList(),
                "Компания" => _dbContext.Companies.ToList(),
                _ => _dbContext.Motorcycles.ToList()
            };

            List.ItemsSource = data;
        }

        [RelayCommand]
        private void OnLoad(string table)
        {
            Load(table);   
        }

        public void Load(string table)
        {
            CurrentTable = table;
            foreach (var t in TableTypes)
            {
                var localized = t.GetCustomAttribute(typeof(LocalizedColumnAttribute)) as LocalizedColumnAttribute;
                if (localized != null)
                {
                    if (localized.Name == table)
                    {
                        LoadColumns(t);
                        LoadData(localized.Name);
                        break;
                    }
                }
            }
        }

        private Type GetPageType(string table)
        {
            return table switch
            {
                "Компания" => typeof(CompanyPage),
                "Фары" => typeof(HeadlightsPage),
                "Шины" => typeof(TiresPage),
                "Мотоцикл" => typeof(MotorcyclePage),
                "Человек" => typeof(PersonPage),
                _ => typeof(MotorcyclePage)
            };
        }

        private IModelForm GetModelForm(string table)
        {
            return table switch
            {
                "Компания" => _companyViewModel,
                "Фары" => _headlightsViewModel,
                "Шины" => _tiresViewModel,
                "Мотоцикл" => _motorcycleViewModel,
                "Человек" => _personViewModel,
                _ => _motorcycleViewModel,
            };
        }

        [RelayCommand]
        private void OnAdd()
        {
            var form = GetModelForm(CurrentTable);
            form.SetMode("Add");
            form.Reset();
            _navigationWindow.Navigate(GetPageType(CurrentTable));
        }

        [RelayCommand]
        private void OnEdit()
        {
            var form = GetModelForm(CurrentTable);
            form.SetMode("Edit");
            form.Load(List.SelectedItem);
            _navigationWindow.Navigate(GetPageType(CurrentTable));
        }

        [RelayCommand]
        private void OnReload()
        {
            Load(CurrentTable);
        }

        [RelayCommand]
        private void OnDelete()
        {
            if (List.SelectedItem != null)
            {
                _dbContext.Remove(List.SelectedItem);
                _dbContext.SaveChanges();
                Load(CurrentTable);
            }
        }
    }
}
