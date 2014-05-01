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

namespace WMS.CustomControls.Controls.CustomSearchTextBox
{
	/// <summary>
	/// Interaction logic for CustomSearchTextBox.xaml
	/// </summary>
	public partial class CustomSearchTextBox : UserControl
	{

		public string Text
		{
			get { return SearchTextBox.Text; }
			set { SearchTextBox.Text = value; }
		}

		public CustomSearchTextBox()
		{
			InitializeComponent();
		}

		private void SearchTextBox_OnGotFocus(object sender, RoutedEventArgs e)
		{
			SearchTextBlock.Visibility = Visibility.Hidden;
		}

		private void SearchTextBox_OnLostFocus(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Text))
			{
				SearchTextBlock.Visibility = Visibility.Visible;
			}
		}
	}
}
