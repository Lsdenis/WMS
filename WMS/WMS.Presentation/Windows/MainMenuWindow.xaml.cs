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
			WindowStartupLocation = WindowStartupLocation.Manual;
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
			btnWarehouses.Click = WHSCLick;
		}

		private void WHSCLick(object sender, RoutedEventArgs e)
		{
			var window = new WarehousesWindow();
			window.Show();
		}
		private void UMClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new UserManagementWindow();
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
			window.Show();	
		}
		private void CellClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new CellsWindow();
			window.Show();
		}
		private void BasketClick(object sender, RoutedEventArgs routedEventArgs)
		{
			var window = new BasketWindow();
			window.Show();
		}
		private void MainMenuWindow_OnClosed(object sender, EventArgs e)
		{
//			_loginWindow.Close();
		}
	}
}
