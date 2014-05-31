using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IUserCartsRepository
	{
		IEnumerable<UserCart> GetUserCarts();
		IEnumerable<LVUserCarts> GetLVUserCarts();
		IEnumerable<LVUserCarts> GetLVUserCarts(int userId);
	}
}
