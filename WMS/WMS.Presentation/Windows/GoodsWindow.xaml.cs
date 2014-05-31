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
using System.Windows.Shapes;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.PickAndStoreObjects;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for GoodsWindow.xaml
	/// </summary>
	public partial class GoodsWindow : BaseWindow
	{
		public GoodsWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			GetAllGoods();
		}
		private void GetAllGoods()
		{
			using (var uow = new UnitOfWork())
			{
				var goods = uow.Goods.GetLVGoods();

				foreach (var good in goods)
				{
					AddGoodToListView(good);
				}
			}
		}
		private void AddGoodToListView(LVGoodItem good)
		{
			lvGoods.Items.Add(good);
		}
		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				lvGoods.Items.Clear();
				using (var uow = new UnitOfWork())
				{
					var goods = uow.Goods.GetLVGoods().Where(gd => gd.Name.Contains(SearchTextBox.Text));

					foreach (var good in goods)
					{
						AddGoodToListView(good);
					}
				}
			}
		}
		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new GoodWindow(null);
			window.Owner = this;
			window.ShowDialog();
			lvGoods.Items.Clear();
			using (var uow = new UnitOfWork())
			{
				var goods = uow.Goods.GetLVGoods();
				foreach (var good in goods)
				{
					AddGoodToListView(good);
				}
			}
		}
		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var lvGoodItem = lvGoods.SelectedItem as LVGoodItem;
			if (lvGoodItem == null)
			{
				return;
			}

			var result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.Cancel)
			{
				return;
			}

			using (var uow = new UnitOfWork())
			{
				var good = uow.Goods.GetAllGoods().SingleOrDefault(gd => gd.Id == lvGoodItem.Id);
				if (good == null)
				{
					return;
				}

				if (good.GoodsInCells.Count != 0)
				{
					MessageBox.Show("You can not delete this good.");
					return;
				}

				uow.Goods.DeleteGood(good);

				uow.Commit();
			}

			lvGoods.Items.Remove(lvGoodItem);
		}
		private void BtnPick_OnClick(object sender, RoutedEventArgs e)
		{
			if (lvGoods.SelectedItems.Count <= 0)
			{
				return;
			}

//			var result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.OKCancel);
//			if (result == MessageBoxResult.Cancel)
//			{
//				return;
//			}

			var selectedGood = lvGoods.SelectedItem as LVGoodItem;

			var window = new PickWindow(selectedGood);
			window.Owner = this;
			window.ShowDialog();

//			using (var uow = new UnitOfWork())
//			{
//				foreach (var lvGoodItem in goodsItems)
//				{
//					if (!uow.PickAndStoreValidationService.IsPickValid(lvGoodItem))
//					{
//						return;
//					}
//				}
//
//				foreach (var lvGoodItem in goodsItems)
//				{
//					uow.PickAndStoreService.PickGood(lvGoodItem);
//				}
//
//				uow.Commit();
//			}
		}
	}
}
