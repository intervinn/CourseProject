using CourseProject.ViewModels.Pages;
using System.Windows.Controls;
using Wpf.Ui.Abstractions.Controls;

namespace CourseProject.Views.Pages
{
    public partial class TiresPage : INavigableView<TiresViewModel>
    {
        public TiresViewModel ViewModel { get; }

        public TiresPage(TiresViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            DataContext = this;
        }
    }
}