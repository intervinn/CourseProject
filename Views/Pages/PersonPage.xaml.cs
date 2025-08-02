using CourseProject.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace CourseProject.Views.Pages
{
    public partial class PersonPage : INavigableView<PersonViewModel>
    {
        public PersonViewModel ViewModel { get; }

        public PersonPage(PersonViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
    }
}