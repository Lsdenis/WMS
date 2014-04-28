using System.Windows;
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for LoginWindow.xaml
	/// </summary>
	public partial class LoginWindow : BaseWindow
	{
		public LoginWindow()
		{
			InitializeComponent();
			BlueButton.Click = BBClick;
			RedButton.Click = (sender, args) => Close();
		}

		private void BBClick(object sender, RoutedEventArgs routedEventArgs)
		{
			using (var uow = new UnitOfWork())
			{
				if (!uow.Users.IsUserExists(txtLogin.Text, txtPassword.Text))
				{
					return;
				}
			}

			var window = new MainMenuWindow();
			window.Show();
			Hide();
		}
	}
}
