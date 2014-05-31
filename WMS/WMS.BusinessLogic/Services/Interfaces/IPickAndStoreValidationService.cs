using WMS.BusinessLogic.PickAndStoreObjects;

namespace WMS.BusinessLogic.Services.Interfaces
{
	public interface IPickAndStoreValidationService
	{
		bool IsPickValid(PickObject pickAndStoreObject);
		bool IsStoreValid(PickObject pickAndStoreObject);
	}
}
