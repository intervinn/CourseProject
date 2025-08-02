using CourseProject.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace CourseProject.Views.Pages
{
    public partial class MotorcyclePage : INavigableView<MotorcycleViewModel>
    {
        public MotorcycleViewModel ViewModel { get; }

        public MotorcyclePage(MotorcycleViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
    }
}