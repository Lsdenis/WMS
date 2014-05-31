using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WMS.CustomControls.Controls.Buttons
{
	/// <summary>
	/// Interaction logic for CustomSaveAndCloseButtons.xaml
	/// </summary>
	public partial class CustomSaveAndCloseButtons : UserControl
	{
		private readonly Thickness _saveCloseVisible = new Thickness(0, 0, 171, 0);
		private readonly Thickness _saveCloseNotVisible = new Thickness(90, 0, 81, 0);

		[DefaultValue(true)]
		public bool IsSaveVisible
		{
			set
			{
				btnSave.Visibility = value ? Visibility.Visible : Visibility.Hidden;
			}
		}

		[DefaultValue(true)]
		public bool IsSaveAndCloseVisible
		{
			set
			{
				if (value)
				{
					btnSaveAndClose.Visibility = Visibility.Visible;
					btnSave.Margin = _saveCloseVisible;
				}
				else
				{
					btnSaveAndClose.Visibility = Visibility.Hidden;
					btnSave.Margin = _saveCloseNotVisible;
				}
			}
		}

		public RoutedEventHandler SaveButtonClick
		{
			set { btnSave.Click += value; }
		}
		public RoutedEventHandler SaveAndCloseButtonClick
		{
			set { btnSaveAndClose.Click += value; }
		}
		public RoutedEventHandler CloseButtonClick
		{
			set { btnClose.Click += value; }
		}
		public CustomSaveAndCloseButtons()
		{
			InitializeComponent();
		}
	}
}
