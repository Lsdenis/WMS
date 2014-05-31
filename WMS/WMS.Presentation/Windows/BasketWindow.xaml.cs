using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
				var userCarts = uow.UserCarts.GetLVUserCarts(CurrentUser.Id);

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

		private void BtnShip_OnClick(object sender, RoutedEventArgs e)
		{
			var result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.Cancel)
			{
				return;
			}
			

			using (var uow = new UnitOfWork())
			{
				var goods = uow.Goods.GetAllGoods();

				foreach (LVUserCarts userCarts in lvItems.SelectedItems)
				{
					var conId = goods.Single(gd => gd.UserCarts.Any(uc => uc.Id == userCarts.Id)).ConsignmentId;

					uow.PickAndStoreService.ShipGood(userCarts.Id);

					var consignment =
						uow.Consignments.GetListOfConsignments().SingleOrDefault(con => con.Id == conId);
					Debug.Assert(consignment != null);
					var count = consignment.Goods.Sum(gd => gd.Count);

					if (count <= 0)
					{
						var resultDelete = MessageBox.Show("There are no more goods with consignment " + consignment + ". Do you want delete?", "Warning!", MessageBoxButton.OKCancel);
						if (resultDelete == MessageBoxResult.Cancel)
						{
							continue;
						}
						uow.Consignments.Delete(consignment);
					}
				}

				uow.Commit();
			}

			SetUserCard();
		}
	}
}
