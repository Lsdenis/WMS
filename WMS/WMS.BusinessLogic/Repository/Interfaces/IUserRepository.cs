using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IUserRepository
	{
		List<User> GetListofUsers();
		void AddUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);
		bool IsUserExists(User user);
		bool IsUserExists(string login, string password);
		bool IsUserLogined(User user);
	}
}
