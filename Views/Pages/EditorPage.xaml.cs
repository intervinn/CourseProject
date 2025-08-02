using CourseProject.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace CourseProject.Views.Pages
{
    /// <summary>
    /// Interaction logic for EditorPage.xaml
    /// </summary>
    public partial class EditorPage : INavigableView<EditorViewModel>
    {
        public EditorViewModel ViewModel { get; }

        public EditorPage(EditorViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            ViewModel.Grid = grid;
            ViewModel.List = list;
            ViewModel.Load("Мотоцикл");
        }
    }
}
