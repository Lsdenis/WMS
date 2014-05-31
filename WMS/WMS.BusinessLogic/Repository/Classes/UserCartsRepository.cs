using System.Collections.Generic;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	public class UserCartsRepository : IUserCartsRepository
	{
		private readonly WarehouseEntities _context;

		public UserCartsRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public IEnumerable<UserCart> GetUserCarts()
		{
			return _context.UserCarts.ToList();
		}

		public IEnumerable<LVUserCarts> GetLVUserCarts()
		{
			return _context.UserCarts.Select(uc => new LVUserCarts
			{
				GoodName = uc.Good.Name,
				GoodConsignment = uc.Good.Consignment.Name,
				Count = uc.Count,
				Id = uc.Id
			}).ToList();
		}

		public IEnumerable<LVUserCarts> GetLVUserCarts(int userId)
		{
			return _context.UserCarts.Where(uc => uc.UserId == userId).Select(uc => new LVUserCarts
			{
				GoodName = uc.Good.Name,
				GoodConsignment = uc.Good.Consignment.Name,
				Count = uc.Count,
				Id = uc.Id
			}).ToList();
		}
	}
}
