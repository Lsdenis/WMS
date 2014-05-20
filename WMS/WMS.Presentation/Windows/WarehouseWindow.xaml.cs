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
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for WarehouseWindow.xaml
	/// </summary>
	public partial class WarehouseWindow : BaseWindow
	{
		private Warehouse _warehouse;

		public WarehouseWindow(Warehouse warehouse)
		{
			_warehouse = warehouse;
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
		}

		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsWarehouseValid())
			{
				return;
			}

			_warehouse.Name = txtName.Text;
			if (chkIsDefault.IsChecked != null)
			{
				_warehouse.IsDefault = (bool) chkIsDefault.IsChecked;
			}

			using (var uow = new UnitOfWork())
			{
				var whs = uow.Warehouses.GetWarehouses();

				foreach (var warehouse in whs)
				{
					warehouse.IsDefault = false;
					uow.Warehouses.UpdateWarehouse(warehouse);
				}

				if (_warehouse.Id == 0)
				{
					uow.Warehouses.AddWarehouse(_warehouse);
				}
				else
				{
					uow.Warehouses.UpdateWarehouse(_warehouse);
				}

				uow.Commit();
			}

			Close();
		}

		private bool IsWarehouseValid()
		{
			if (string.IsNullOrWhiteSpace(txtName.Text))
			{
				return false;
			}

			if (IsWarehouseExists())
			{
				return false;
			}

			return true;
		}

		private bool IsWarehouseExists()
		{
			using (var uow = new UnitOfWork())
			{
				return uow.Warehouses.GetWarehouses().Any(whs => whs.Name.Equals(txtName.Text, StringComparison.OrdinalIgnoreCase) && whs.Id != _warehouse.Id);
			}
		}

		private void WarehouseWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (_warehouse == null)
			{
				_warehouse = new Warehouse();
				_warehouse.IsDefault = false;
			}
			else
			{
				SetWarehouseValues();
			}
		}

		private void SetWarehouseValues()
		{
			txtName.Text = _warehouse.Name;
			chkIsDefault.IsChecked = _warehouse.IsDefault;
		}
	}
}
