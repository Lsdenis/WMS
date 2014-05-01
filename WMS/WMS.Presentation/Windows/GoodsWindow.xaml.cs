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
			RedButton.Click = (sender, args) => Close();
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
	}
}
