using System;
using System.Windows;
using System.Windows.Controls;

namespace WMS.CustomControls.Controls.Buttons
{
	/// <summary>
	/// Interaction logic for BlueButton.xaml
	/// </summary>
	public partial class RedButton : UserControl
	{
		public string Text
		{
			set { btnRedButton.Content = value; }
		}

		public RoutedEventHandler Click
		{
			set { btnRedButton.Click += value; }
		}

		public RedButton()
		{
			InitializeComponent();
		}
	}
}
