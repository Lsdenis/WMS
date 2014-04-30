using System.Linq;
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
			BlueButton.Click = Click;
			RedButton.Click = (sender, args) => Close();
		}

		private void Click(object sender, RoutedEventArgs routedEventArgs)
		{
			using (var uow = new UnitOfWork())
			{
				var users = uow.Users.GetListOfUsers();

				CurrentUser = users.FirstOrDefault(usr => usr.Login.Equals(txtLogin.Text) && usr.Password.Equals(txtPassword.Text));

				if (CurrentUser == null)
				{
					return;
				}
			}

			var window = new MainMenuWindow(this);
			window.Show();
			Hide();
		}
	}
}
