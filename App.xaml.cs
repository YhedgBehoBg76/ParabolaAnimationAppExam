using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ParabolaAnimationApp.Services;
using ParabolaAnimationApp.ViewModels;
using ParabolaAnimationApp.Views;

namespace ParabolaAnimationApp
{
    public partial class App : Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Настройка DI
            var services = new ServiceCollection();

            // Регистрируем контекст БД как Singleton (один на всё приложение)
            services.AddDbContext<AppDbContext>(ServiceLifetime.Singleton);

            // Регистрируем сервис
            services.AddSingleton<ILaunchDatabase, LaunchDatabase>();

            // Регистрируем ViewModel
            services.AddSingleton<MainViewModel>();

            _serviceProvider = services.BuildServiceProvider();

            // Создаём главное окно, внедряем ViewModel
            var mainWindow = new MainWindow
            {
                DataContext = _serviceProvider.GetRequiredService<MainViewModel>()
            };
            mainWindow.Show();
        }
    }
}