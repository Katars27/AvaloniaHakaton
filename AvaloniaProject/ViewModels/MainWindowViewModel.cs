using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaProject.ViewModels;
using AvaloniaProject;
using CommunityToolkit.Mvvm.Input;
using SevenZip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AvaloniaApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ICommand CompressCommand { get; }
        public ICommand DecompressCommand { get; }

        public MainWindowViewModel()
        {
            CompressCommand = new RelayCommand(_ => Compress());
        }

        public string StatusMessage { get; set; }

        public async void Compress()
        {
            try
            {
                // Получаем главное окно приложения
                var mainWindow = (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.MainWindow;
                if (mainWindow == null)
                {
                    StatusMessage = "Главное окно не найдено.";
                    return;
                }

                // Выбор файлов или папки
                var folderDialog = new OpenFolderDialog { Title = "Выберите папку для архивации" };
                var folderPath = await folderDialog.ShowAsync(mainWindow);

                if (string.IsNullOrEmpty(folderPath))
                {
                    StatusMessage = "Папка для архивации не выбрана.";
                    return;
                }

                // Указание имени архива
                var saveFileDialog = new SaveFileDialog
                {
                    Title = "Сохраните архив",
                    InitialFileName = "archive.7z",
                    Filters = new List<FileDialogFilter>
                    {
                        new FileDialogFilter { Name = "7-Zip Archive", Extensions = new List<string> { "7z" } }
                    }
                };
                var archivePath = await saveFileDialog.ShowAsync(mainWindow);

                if (string.IsNullOrEmpty(archivePath))
                {
                    StatusMessage = "Имя архива не указано.";
                    return;
                }

                // Установка пути к библиотеке 7z.dll
                var dllPath = Path.Combine(AppContext.BaseDirectory, "NativeBinaries", "7z.dll");
                if (!File.Exists(dllPath))
                {
                    StatusMessage = $"Библиотека {dllPath} не найдена!";
                    return;
                }
                SevenZipBase.SetLibraryPath(dllPath);

                // Архивация папки
                var compressor = new SevenZipCompressor
                {
                    CompressionLevel = CompressionLevel.High
                };
                compressor.CompressDirectory(folderPath, archivePath);

                StatusMessage = $"Папка {folderPath} упакована в {archivePath}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ошибка архивации: {ex.Message}";
            }
        }

    }
}
