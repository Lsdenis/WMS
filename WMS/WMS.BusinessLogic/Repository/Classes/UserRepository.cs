using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	class UserRepository: IUserRepository
	{
		private readonly WarehouseEntities _context;

		public UserRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public List<User> GetListOfUsers()
		{
			return _context.Users.ToList();
		}

		public void AddUser(User user)
		{
			_context.Users.Add(user);
		}

		public void UpdateUser(User user)
		{
			var existingUser = _context.Users.FirstOrDefault(usr => usr.Id == user.Id);
			if (existingUser == null)
			{
				return;
			}
			_context.Entry(existingUser).CurrentValues.SetValues(user);
			_context.Entry(existingUser).State = EntityState.Modified;;
		}

		public void DeleteUser(User user)
		{
			var existingUser = _context.Users.FirstOrDefault(usr => usr.Id == user.Id);
			if (existingUser == null)
			{
				return;
			}
			_context.Users.Remove(existingUser);
		}
	}
}
