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

namespace WMS.CustomControls.Controls
{
	/// <summary>
	/// Interaction logic for LinkLabel.xaml
	/// </summary>
	public partial class LinkLabel : UserControl
	{
		public string Text
		{
			set { btnLink.Content = value; }
		}
		public LinkLabel()
		{
			InitializeComponent();
		}
	}
}
