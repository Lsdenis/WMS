using System.Windows;
using WMS.BusinessLogic.DataModel;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for GoodWindow.xaml
	/// </summary>
	public partial class GoodWindow: BaseWindow
	{
		private Good _good;
		public GoodWindow(Good good)
		{
			InitializeComponent();
			_good = good;
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
			}
		}

		private void SetGoodValues()
		{
				
		}
	}
}
