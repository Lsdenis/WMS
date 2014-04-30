using System;
using System.Windows;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for MainMenuWindow.xaml
	/// </summary>
	public partial class MainMenuWindow : BaseWindow
	{
		private readonly Window _loginWindow;
		public MainMenuWindow(Window loginWindow)
		{
			InitializeComponent();
			_loginWindow = loginWindow;
			btnUser.Text = CurrentUser.Login;
			SetButtonClicks();
		}

		private void SetButtonClicks()
		{
			btnBasket.Click = BasketClick;
			btnCells.Click = CellClick;
			btnGoods.Click = GoodClick;
			btnUser.Click = UserClick;
			btnUserManagement.Click = UMClick;
		}

		private void UMClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new UserManagementWndow();
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			window.Show();
		}

		private void UserClick(object sender, RoutedEventArgs routedEventArgs)
		{
			_loginWindow.Show();
			Close();
		}

		private void GoodClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new GoodsWindow();
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			window.Show();	
		}

		private void CellClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new CellsWindow();
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			window.Show();
		}

		private void BasketClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new BasketWindow();
			window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
			window.Show();
		}

		private void MainMenuWindow_OnClosed(object sender, EventArgs e)
		{
			_loginWindow.Close();
		}
	}
}
