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
using WMS.BusinessLogic.UnitOfwork;
using WMS.CustomControls.Controls.BaseWindow;

namespace WMS.Presentation.Windows
{
	/// <summary>
	/// Interaction logic for UserWindow.xaml
	/// </summary>
	public partial class UserWindow : BaseWindow
	{
		private User _user;

		public UserWindow(User user)
		{
			InitializeComponent(); 
			WindowStartupLocation = WindowStartupLocation.CenterOwner;
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
			ucSaveAndClose.SaveAndCloseButtonClick = SaveAndCloseButtonClick;
			_user = user;
		}
		private void SaveAndCloseButtonClick(object sender, RoutedEventArgs routedEventArgs)
		{
			if (!IsUserDataValid())
			{
				return;
			}
			GetUsersValues();
			SaveUserValues();
			Close();
		}
		private void SaveUserValues()
		{
			using (var uow = new UnitOfWork())
			{
				if (_user.Id == 0)
				{
					uow.Users.AddUser(_user);
				}
				else
				{
					uow.Users.UpdateUser(_user);
				}

				uow.Commit();
			}
		}
		private bool IsUserDataValid()
		{
			if (string.IsNullOrWhiteSpace(txtLogin.Text))
			{
				MessageBox.Show("Login should not be empty!", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtPassword.Text))
			{
				MessageBox.Show("Password should not be empty!", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (string.IsNullOrWhiteSpace(txtConfirm.Text))
			{
				MessageBox.Show("You should confirm your password!", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (!string.Equals(txtPassword.Text, txtConfirm.Text))
			{
				MessageBox.Show("Your confirmation is not correct!", "Warning!", MessageBoxButton.OK);
				return false;
			}

			if (IsUserExists())
			{
				MessageBox.Show("User with the same login already exists!", "Warning!", MessageBoxButton.OK);
				return false;
			}

			return true;

		}

		private bool IsUserExists()
		{
			using (var uow = new UnitOfWork())
			{
				return uow.Users.GetListOfUsers().Any(usr => usr.Login.Equals(txtLogin.Text));
			}
		}

		private void GetUsersValues()
		{
			_user.Login = txtLogin.Text;
			_user.Password = txtPassword.Text;
			_user.LastLoginedDate = DateTime.Now.Date;
		}
		private void UserWindow_OnLoaded(object sender, RoutedEventArgs e)
		{
			if (_user == null)
			{
				_user = new User();
			}
			else
			{
				SetUsersValues();
			}
		}
		private void SetUsersValues()
		{
			txtLogin.Text = _user.Login;
		}
	}
}
