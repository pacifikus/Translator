using System.Windows;
using Translator.ViewModel;

namespace Translator
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new Presenter();
		}
	}
}
