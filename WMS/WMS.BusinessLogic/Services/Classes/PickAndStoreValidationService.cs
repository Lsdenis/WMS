using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.PickAndStoreObjects;
using WMS.BusinessLogic.Services.Interfaces;

namespace WMS.BusinessLogic.Services.Classes
{
	public class PickAndStoreValidationService : IPickAndStoreValidationService
	{
		private readonly WarehouseEntities _context;

		public PickAndStoreValidationService(WarehouseEntities context)
		{
			_context = context;
		}

		public bool IsPickValid(PickAndStoreObject pickAndStoreObject)
		{
			return true;
		}

		public bool IsStoreValid(PickAndStoreObject pickAndStoreObject)
		{
			return true;	
		}
	}
}
