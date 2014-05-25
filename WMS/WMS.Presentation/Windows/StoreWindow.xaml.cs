using System;
using System.Collections.Generic;
using System.Linq;
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
	/// Interaction logic for StoreWindow.xaml
	/// </summary>
	public partial class StoreWindow : BaseWindow
	{
		private LVUserCarts _lvUserCarts;

		private Dictionary<int, int> _availableValueInCell = new Dictionary<int, int>();

		public StoreWindow(LVUserCarts lvUserCarts)
		{
			_lvUserCarts = lvUserCarts;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			InitializeComponent();
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
		}

		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsStoreValid())
			{
				return;
			}

			var cellId = ((Cell)cbCells.SelectedItem).Id;
			var cardId = _lvUserCarts.Id;
			var count = int.Parse(txtCount.Text);

			var storeObject = new StoreObject(cardId, cellId, count);

			using (var uow = new UnitOfWork())
			{
				uow.PickAndStoreService.StoreGood(storeObject);
				uow.Commit();
			}

			Close();
		}

		private bool IsStoreValid()
		{
			if (!IsCountValid())
			{
				return false;
			}

			var cell = cbCells.SelectedItem as Cell;
			var count = int.Parse(txtCount.Text);
			if (cell == null)
			{
				MessageBox.Show("No cell available.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (_lvUserCarts.Count < count)
			{
				MessageBox.Show("You don't have enough items.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (_availableValueInCell[cell.Id] < count)
			{
				MessageBox.Show("Count can not be more than available place.", "Warning!", MessageBoxButton.OK);
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

		private void StoreWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			using (var uow = new UnitOfWork())
			{
				var cells = uow.Cells.GetCells();

				foreach (var cell in cells)
				{
					var count = cell.GoodsInCells.Sum(gic => gic.Value);

					if (count == cell.MaxValue)
					{
						continue;
					}

					cbCells.Items.Add(cell);
					_availableValueInCell.Add(cell.Id, cell.MaxValue - count);
				}
			}

			if (cbCells.Items.Count > 0)
			{
				cbCells.SelectedIndex = 0;
			}
		}

		private void CbCells_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedCell = cbCells.SelectedItem as Cell;
			if (selectedCell == null)
			{
				return;
			}

			lblAvailable.Text = _availableValueInCell[selectedCell.Id].ToString();
		}
	}
}
