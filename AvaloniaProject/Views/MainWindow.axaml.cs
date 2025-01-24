using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Threading;
using AvaloniaProject.ViewModels;
using System;

namespace AvaloniaProject.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();

            this.Opacity = 0; // Прозрачность
            this.RenderTransform = new ScaleTransform(0.5, 0.5); 

            // Анимация появления
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16) 
            };

            double opacityStep = 0.05; // Скорость увеличения прозрачности
            double scaleStep = 0.01; // Скорость увеличения масштаба

            timer.Tick += (sender, args) =>
            {
                if (this.Opacity < 1)
                {
                    this.Opacity += opacityStep;
                }

                if (this.RenderTransform is ScaleTransform scale)
                {
                    if (scale.ScaleX < 1)
                    {
                        scale.ScaleX += scaleStep;
                        scale.ScaleY += scaleStep;
                    }
                }

                if (this.Opacity >= 1 && (this.RenderTransform as ScaleTransform)?.ScaleX >= 1)
                {
                    timer.Stop();
                }
            };

            timer.Start();
        }

        private void OnCompressClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm?.Compress();
        }

        private void OnDecompressClick(object? sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainWindowViewModel;
            vm?.Decompress();
        }
    }
}