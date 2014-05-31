using System.Linq;
using System.Windows;
using System.Windows.Input;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for WarehousesWindow.xaml
	/// </summary>
	public partial class WarehousesWindow : BaseWindow
	{
		public WarehousesWindow()
		{
			InitializeComponent();
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new WarehouseWindow(null);
			window.Owner = this;
			window.ShowDialog();
			SetWarehouses();
		}

		private void WarehousesWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			SetWarehouses();
		}

		private void SetWarehouses()
		{
			lvWarehouses.Items.Clear();
			using (var uow = new UnitOfWork())
			{
				var whs = uow.Warehouses.GetWarehouses();

				foreach (var warehouse in whs)
				{
					lvWarehouses.Items.Add(warehouse);
				}
			}
		}

		private void LvWarehouses_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var currentWarehouse = lvWarehouses.SelectedItem as Warehouse;
			if (currentWarehouse == null)
			{
				return;
			}

			var window = new WarehouseWindow(currentWarehouse);
			window.Owner = this;
			window.ShowDialog();

			SetWarehouses();
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key != Key.Return)
			{
				return;
			}

			lvWarehouses.Items.Clear();
			using (var uow = new UnitOfWork())
			{
				var whs = uow.Warehouses.GetWarehouses().Where(wh => wh.Name.Contains(SearchTextBox.Text));

				foreach (var warehouse in whs)
				{
					lvWarehouses.Items.Add(warehouse);
				}
			}
		}
	}
}
