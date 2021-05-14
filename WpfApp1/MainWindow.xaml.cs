using Domain.Entities;
using Kingston_Master_of_Tea_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kingston_Master_of_Tea_WPF_UI
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MoTClient _client;

		//public NotifyTaskCompletion<List<BeverageItem>> Baverages { get; private set; }

		public MainWindow(MoTClient client)
		{
			_client = client;
			//Baverages = new NotifyTaskCompletion<List<BeverageItem>>(
			//	_client.GetBeverageItem());
			Initialize();
			InitializeComponent();
		}
		private async void Initialize()
		{
			Baverages.ItemsSource = await  _client.GetBeverageItem();
		}
	}
}
