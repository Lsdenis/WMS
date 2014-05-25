using System.Collections.Generic;
using System.Windows;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for BasketWindow.xaml
	/// </summary>
	public partial class BasketWindow : BaseWindow
	{
		public BasketWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
		}

		private void BasketWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			SetUserCard();
		}

		private void SetUserCard()
		{
			lvItems.Items.Clear();
			using (var uow = new UnitOfWork())
			{
				var userCarts = uow.UserCartsRepository.GetLVUserCarts(CurrentUser.Id);

				AddUserCarts(userCarts);
			}
		}

		private void AddUserCarts(IEnumerable<LVUserCarts> userCarts)
		{
			lvItems.Items.Clear();

			foreach (var userCart in userCarts)
			{
				lvItems.Items.Add(userCart);
			}
		}

		private void BtnStore_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedGood = lvItems.SelectedItem as LVUserCarts;
			if (selectedGood == null)
			{
				return;
			}

			var window = new StoreWindow(selectedGood);
			window.Owner = this;
			window.ShowDialog();

			SetUserCard();
		}
	}
}
