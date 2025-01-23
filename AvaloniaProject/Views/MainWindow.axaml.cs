using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaProject.ViewModels;

namespace AvaloniaProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnCompressClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm?.Compress();
            StatusText.Text = vm?.StatusMessage;
        }
        private void OnDecompressClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm?.Decompress();
            StatusText.Text = vm?.StatusMessage;
        }
    }
}