using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IGoodsRepository
	{
		List<Good> GetAllGoods();
		void AddGood(Good good);
		void UpdateGood(Good good);
		void DeleteGood(Good good);

		List<LVGoodItem> GetLVGoods();
	}
}
