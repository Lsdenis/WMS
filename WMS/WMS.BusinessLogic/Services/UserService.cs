using System;
using System.Linq;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Services
{
	public class UserService
	{
		private readonly WarehouseEntities _context;
		public UserService(WarehouseEntities context)
		{
			_context = context;
		}

		public bool IsUserExists(string login, string password)
		{
			if (
				_context.Users.Any(
					usr =>
						usr.Login.Equals(login, StringComparison.OrdinalIgnoreCase) &&
						usr.Password.Equals(password, StringComparison.OrdinalIgnoreCase)))
			{
				return true;
			}

			return false;
		}
	}
}
