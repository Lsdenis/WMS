using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.PickAndStoreObjects;

namespace WMS.BusinessLogic.Services.Interfaces
{
	public interface IPickAndStoreService
	{
		void PickGood(PickObject pickObject);
		void StoreGood(StoreObject storeObject);
		void ShipGood(int userCartId);
	}
}
