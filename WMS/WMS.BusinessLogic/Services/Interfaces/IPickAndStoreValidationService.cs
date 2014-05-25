using WMS.BusinessLogic.PickAndStoreObjects;

namespace WMS.BusinessLogic.Services.Interfaces
{
	public interface IPickAndStoreValidationService
	{
		bool IsPickValid(PickAndStoreObject pickAndStoreObject);
		bool IsStoreValid(PickAndStoreObject pickAndStoreObject);
	}
}
