using System.Windows;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MainMenuWindow.xaml
    /// </summary>
    public partial class MainMenuWindow : BaseWindow
    {
        public MainMenuWindow()
        {
            InitializeComponent();
        }

	    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
	    {
		    MessageBox.Show("ToolBar button clicked");
	    }
    }
}
