using System;
using System.Linq;
using System.Windows;
using WMS.BusinessLogic.DataModel;
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
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			BlueButton.Click = Click;
			RedButton.Click = (sender, args) => Close();
		}
		private void Click(object sender, RoutedEventArgs routedEventArgs)
		{
			if ( IsUserValues() || IsAdminValues())
			{
				ShowMainWindow();
			}
		}
		private bool IsUserValues()
		{
			using (var uow = new UnitOfWork())
			{
				var users = uow.Users.GetListOfUsers();

				CurrentUser = users.FirstOrDefault(usr => usr.Login.Equals(txtLogin.Text) && usr.Password.Equals(txtPassword.Text));

				if (CurrentUser == null)
				{
					return false;
				}

				CurrentUser.LastLoginedDate = DateTime.Now.Date;
				uow.Users.UpdateUser(CurrentUser);
				uow.Commit();
			}
			return true;
		}
		private bool IsAdminValues()
		{
			if (!txtLogin.Text.Equals("Admin") || !txtPassword.Text.Equals("Admin"))
			{
				return false;
			}
			CurrentUser = new User
			{
				Login = "Admin"
			};
			return true;
		}
		private void ShowMainWindow()
		{
			var window = new MainMenuWindow(this);
			window.Show();
			Hide();
			txtLogin.Text = txtPassword.Text = "";
		}
	}
}
