using System;
using System.Globalization;
using System.Windows;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for GoodWindow.xaml
	/// </summary>
	public partial class GoodWindow : BaseWindow
	{
		private Good _good;
		public GoodWindow(Good good)
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			_good = good;
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
			ucSaveAndClose.CloseButtonClick = CloseButtonClick;
		}
		private void CloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			Close();
		}
		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsGoodValuesValidated())
			{
				return;
			}

			GetGoodValues();

			using (var uow = new UnitOfWork())
			{
				if (_good.Id == 0)
				{
					uow.Goods.AddGood(_good);
				}
				else
				{
					uow.Goods.UpdateGood(_good);
				}

				uow.Commit();
			}

			Close();
		}
		private void GetGoodValues()
		{
			_good.Count = int.Parse(txtCount.Text);
			_good.Name = txtName.Text;
			_good.ConsignmentId = ((Consignment)cbConsignment.SelectedItem).Id;
			_good.AddingDate = DateTime.Now.Date;
		}
		private bool IsGoodValuesValidated()
		{
			if (string.IsNullOrWhiteSpace(txtName.Text))
			{
				MessageBox.Show("Name should not be null", "Warning!", MessageBoxButton.OK);
				return false;
			}

			int result;
			if (!int.TryParse(txtCount.Text, NumberStyles.Integer, new NumberFormatInfo(), out result))
			{
				MessageBox.Show("Count should be integer.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (!(cbConsignment.SelectedItem is Consignment))
			{
				MessageBox.Show("Unable to save good without consignment.", "Warning!", MessageBoxButton.OK);
				return false;
			}

			return true;
		}
		private void GoodWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (_good != null)
			{
				SetGoodValues();
			}
			else
			{
				_good = new Good();

				SetConsignmentValues();

				lblAddingDate.Content = DateTime.Now.Date.ToShortDateString();
			}
		}
		private void SetConsignmentValues()
		{
			using (var uow = new UnitOfWork())
			{
				var consignments = uow.Consignments.GetListOfConsignments();

				foreach (var consignment in consignments)
				{
					cbConsignment.Items.Add(consignment);

				}
				if (cbConsignment.Items.Count > 0)
				{
					cbConsignment.SelectedIndex = 0;
				}
			}
		}
		private void SetGoodValues()
		{
			txtCount.Text = _good.Count.ToString();
			txtName.Text = _good.Name;
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new ConsignmentWindow();
			window.Owner = this;
			window.ShowDialog();
			SetConsignmentValues();
		}
	}
}
