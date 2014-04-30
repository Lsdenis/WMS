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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WMS.CustomControls.Controls.CustomToolBarItem
{
	/// <summary>
	/// Interaction logic for CustomToolBarItem.xaml
	/// </summary>
	public partial class CustomToolBarItem : UserControl
	{
		public string Text
		{
			get { return ToolBarTextBlock.Text; }
			set { ToolBarTextBlock.Text = value; }
		}

		public ImageSource ButtonImage
		{
			set
			{
				if (value != null)
				{
//					ToolBarButton.Content = value;
					ToolBarButton.Background = new ImageBrush { ImageSource = value };
					//					Image.Source = value;
				}
			}
		}

		public RoutedEventHandler Click
		{
			set { ToolBarButton.Click += value; }
		}

		public CustomToolBarItem()
		{
			InitializeComponent();
		}
	}
}
