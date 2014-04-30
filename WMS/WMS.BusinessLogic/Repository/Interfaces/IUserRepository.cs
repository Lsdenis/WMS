using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IUserRepository
	{
		List<User> GetListOfUsers();
		void AddUser(User user);
		void UpdateUser(User user);
		void DeleteUser(User user);
	}
}
