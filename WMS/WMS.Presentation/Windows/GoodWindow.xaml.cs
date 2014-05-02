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
				uow.Goods.AddGood(_good);

				uow.Commit();
			}
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
			}
		}
		private void SetGoodValues()
		{
			txtCount.Text = _good.Count.ToString();
			txtName.Text = _good.Name;
		}
	}
}
