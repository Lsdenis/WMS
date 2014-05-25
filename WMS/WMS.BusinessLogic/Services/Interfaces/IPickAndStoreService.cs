using WMS.BusinessLogic.PickAndStoreObjects;

namespace WMS.BusinessLogic.Services.Interfaces
{
	public interface IPickAndStoreService
	{
		void PickGood(PickAndStoreObject pickAndStoreObject);
		void StoreGood(PickAndStoreObject pickAndStoreObject);
	}
}
