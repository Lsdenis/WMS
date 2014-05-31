using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
	/// Interaction logic for UserManagementWindow.xaml
	/// </summary>
	public partial class UserManagementWindow : BaseWindow
	{
		public UserManagementWindow()
		{
			InitializeComponent();
			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			SetUsersInListView();
			ucSaveAndClose.CloseButtonClick = (sender, args) => Close();
		}

		private void SetUsersInListView()
		{
			lvUsers.Items.Clear();
			using (var uow = new UnitOfWork())
			{
				var users = uow.Users.GetListOfUsers();
				foreach (var user in users)
				{
					lvUsers.Items.Add(user);
				}
			}
		}

		private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
		{
			var user = lvUsers.SelectedItem as User;
			if (user == null)
			{
				return;
			}
			var result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.Cancel)
			{
				return;
			}
			using (var uow = new UnitOfWork())
			{
				uow.Users.DeleteUser(user);
				uow.Commit();
			}
			lvUsers.Items.Remove(user);
		}

		private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
		{
			var window = new UserWindow(null);
			window.Owner = this;
			window.ShowDialog();
			lvUsers.Items.Clear();
			SetUsersInListView();
		}

		private void LvUsers_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			var user = lvUsers.SelectedItem as User;
			if (user == null)
			{
				return;
			}

			var window = new UserWindow(user);
			window.Owner = this;
			window.ShowDialog();

			SetUsersInListView();
		}

		private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				lvUsers.Items.Clear();
				using (var uow = new UnitOfWork())
				{
					var users = uow.Users.GetListOfUsers().Where(usr => usr.Login.Contains(SearchTextBox.Text));

					foreach (var user in users)
					{
						lvUsers.Items.Add(user);
					}
				}
			}
		}
	}
}
