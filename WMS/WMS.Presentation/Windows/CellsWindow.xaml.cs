using System.Linq;
using System.Windows;
using System.Windows.Input;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for CellsWindow.xaml
	/// </summary>
	public partial class CellsWindow : BaseWindow
	{
		public CellsWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();

			SetCellsValues();
		}

		private void SetCellsValues()
		{
			using (var uow = new UnitOfWork())
			{
				lvCells.Items.Clear();
				var cells = uow.Cells.GetLVCells();

				foreach (var cell in cells)
				{
					lvCells.Items.Add(cell);
				}
			}
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				lvCells.Items.Clear();
				using (var uow = new UnitOfWork())
				{
					var users = uow.Cells.GetLVCells().Where(cell => cell.Number.ToString().Contains(SearchTextBox.Text));

					foreach (var user in users)
					{
						lvCells.Items.Add(user);
					}
				}
			}
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new CellWindow(null);
			window.Owner = this;
			window.ShowDialog();
			SetCellsValues();
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var currentCell = lvCells.SelectedItem as LVCellsItems;
			if (currentCell == null)
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
				var cell = uow.Cells.GetCell(currentCell.Id);
				uow.Cells.DeleteCell(cell);
				uow.Commit();
			}

			SetCellsValues();
		}

		private void LvCells_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var currentCell = lvCells.SelectedItem as LVCellsItems;
			if (currentCell == null)
			{
				return;
			}

			Cell cell;
			using (var uow = new UnitOfWork())
			{
				cell = uow.Cells.GetCell(currentCell.Id);
			}

			var window = new CellWindow(cell);
			window.Owner = this;
			window.ShowDialog();
			SetCellsValues();
		}
	}
}
