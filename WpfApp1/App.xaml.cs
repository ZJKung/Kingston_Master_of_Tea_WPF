using Kingston_Master_of_Tea_Application;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Kingston_Master_of_Tea_WPF_UI
{
	
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly ServiceProvider _serviceProvider;
		public App()
		{
			ServiceCollection service = new();
			ConfigureServices(service);
			_serviceProvider = service.BuildServiceProvider();
		}
		private void ConfigureServices(ServiceCollection services)
		{
			services.AddHttpClient<MoTClient>();
			services.AddScoped<MoTClient>();
			services.AddSingleton<MainWindow>();
		}

		private void OnStartup(object sender, StartupEventArgs e)
		{
			var mainWindow = _serviceProvider.GetService<MainWindow>();
			mainWindow.Show();
		}
	}
}
