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

		public bool IsPickValid(PickObject pickAndStoreObject)
		{
			return true;
		}

		public bool IsStoreValid(PickObject pickAndStoreObject)
		{
			return true;	
		}
	}
}
