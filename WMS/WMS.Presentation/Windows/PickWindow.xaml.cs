using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.PickAndStoreObjects;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for PickWindow.xaml
	/// </summary>
	public partial class PickWindow : BaseWindow
	{
		private readonly LVGoodItem _selectedGoodItem;
		private Dictionary<int, int> _goodInCells = new Dictionary<int, int>();

		public PickWindow(LVGoodItem selectedGoodItem)
		{
			_selectedGoodItem = selectedGoodItem;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			InitializeComponent();
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
		}

		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsGoodValidForPick())
			{
				return;
			}
			var cell = cbCell.SelectedItem as Cell;
			var cellId = cell == null ? (int?) null : cell.Id;
			var count = int.Parse(txtCount.Text);
			var goodId = _selectedGoodItem.Id;
			var pickObject = new PickObject(goodId, cellId, CurrentUser.Id, count);

			using (var uow = new UnitOfWork())
			{
				uow.PickAndStoreService.PickGood(pickObject);
				uow.Commit();
			}

			Close();
		}

		private bool IsGoodValidForPick()
		{
			if (!IsCountValid())
			{
				return false;
			}

			var count = int.Parse(txtCount.Text);
			var selectedCell = cbCell.SelectedItem as Cell;
			var index = selectedCell == null ? 0 : selectedCell.Id;

			if (count > _selectedGoodItem.Count)
			{
				MessageBox.Show("There are no enugh items.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (count > _goodInCells[index])
			{
				MessageBox.Show("You can not pick more then there are.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			return true;
		}

		private bool IsCountValid()
		{
			int count;
			if (!int.TryParse(txtCount.Text, out count))
			{
				MessageBox.Show("Count should be a number.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (count <= 0)
			{
				MessageBox.Show("Count should be positive.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			return true;
		}

		private void PickWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			using (var uow = new UnitOfWork())
			{
				var good = uow.Goods.GetAllGoods().Single(gd => gd.Id == _selectedGoodItem.Id);
				var cells = good.GoodsInCells.Select(gic => gic.Cell).ToList();
				var count = good.GoodsInCells.Sum(goodsInCell => goodsInCell.Value) + good.UserCarts.Sum(uc => uc.Count);

				if (_selectedGoodItem.Count > count)
				{
					_goodInCells.Add(0, _selectedGoodItem.Count - count);
				}

				foreach (var goodsInCell in good.GoodsInCells)
				{
					_goodInCells.Add(goodsInCell.CellId, goodsInCell.Value);
				}

				lblGoodName.Text = good.Name;
				lblGoodConsignment.Text = good.Consignment.Name;

				AddCellsToComboBox(cells, count);
			}
		}

		private void AddCellsToComboBox(IEnumerable<Cell> cells, int count)
		{
			cbCell.Items.Clear();

			if (_selectedGoodItem.Count > count)
			{
				cbCell.Items.Add("None");
			}

			foreach (var cell in cells)
			{
				cbCell.Items.Add(cell);
			}

			cbCell.SelectedIndex = 0;
		}

		private void CbCell_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedItem = cbCell.SelectedItem as Cell;

			txtGoodsInCell.Text = selectedItem == null ? _goodInCells[0].ToString() : _goodInCells[selectedItem.Id].ToString();
		}
	}
}
