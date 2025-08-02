using CourseProject.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace CourseProject.Views.Pages
{
    public partial class HeadlightsPage : INavigableView<HeadlightsViewModel>
    {
        public HeadlightsViewModel ViewModel { get; }

        public HeadlightsPage(HeadlightsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
    }
}