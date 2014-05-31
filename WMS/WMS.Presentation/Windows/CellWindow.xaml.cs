using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for CellWindow.xaml
	/// </summary>
	public partial class CellWindow : BaseWindow
	{
		private Cell _cell;

		public CellWindow(Cell cell)
		{
			_cell = cell;
			InitializeComponent();
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
		}

		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsCellValid())
			{
				return;
			}

			GetCellValues();
			SaveCell();
			Close();
		}

		private void SaveCell()
		{
			using (var uow = new UnitOfWork())
			{
				if (_cell.Id == 0)
				{
					uow.Cells.AddCell(_cell);
				}
				else
				{
					uow.Cells.UpdateCell(_cell);
				}

				uow.Commit();
			}
		}

		private void GetCellValues()
		{
			_cell.Number = int.Parse(txtNumber.Text);
			_cell.WarehouseId = ((Warehouse)cbWarehouses.SelectedItem).Id;
			_cell.MaxValue = int.Parse(txtMaxValue.Text);
		}

		#region Validation

		private bool IsCellValid()
		{
			if (!IsValuesValid())
			{
				return false;
			}

			if (IsNumberExistsInWarehouse())
			{
				return false;
			}

			return true;
		}

		private bool IsNumberExistsInWarehouse()
		{
			var warehouse = cbWarehouses.SelectedItem as Warehouse;
			var number = int.Parse(txtNumber.Text);

			IEnumerable<Cell> cells;

			using (var uow = new UnitOfWork())
			{
				cells = uow.Cells.GetCells();
			}

			return cells.Any(cell => cell.WarehouseId == warehouse.Id && cell.Number == number && cell.Id != _cell.Id);
		}

		private bool IsValuesValid()
		{
			if (!IsNumberValid())
			{
				return false;
			}

			if (!IsWarehouseValid())
			{
				return false;
			}

			if (!IsMaxValueValid())
			{
				return false;
			}

			return true;
		}

		private bool IsMaxValueValid()
		{
			int result;
			if (!int.TryParse(txtNumber.Text, out result))
			{
				return false;
			}

			if (result <= 0)
			{
				return false;
			}

			return true;

		}

		private bool IsWarehouseValid()
		{
			return cbWarehouses.SelectedItem is Warehouse;
		}

		private bool IsNumberValid()
		{
			int result;
			return int.TryParse(txtNumber.Text, out result);
		}

		#endregion

		private void CellWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			SetWarehousesValues();
			if (_cell == null)
			{
				_cell = new Cell();
			}
			else
			{
				SetCellValues();
			}
		}

		private void SetCellValues()
		{
			txtNumber.Text = _cell.Number.ToString();
			txtMaxValue.Text = _cell.MaxValue.ToString();
			cbWarehouses.SelectedItem = cbWarehouses.Items.Cast<Warehouse>().SingleOrDefault(whs => whs.Id == _cell.WarehouseId);
		}

		private void SetWarehousesValues()
		{
			using (var uow = new UnitOfWork())
			{
				var whs = uow.Warehouses.GetWarehouses();
				foreach (var warehouse in whs)
				{
					cbWarehouses.Items.Add(warehouse);
				}

				if (cbWarehouses.Items.Count > 0)
				{
					cbWarehouses.SelectedIndex = 0;
				}
			}
		}
	}
}
