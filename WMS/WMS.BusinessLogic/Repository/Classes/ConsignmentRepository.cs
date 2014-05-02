using System.Collections.Generic;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	public class ConsignmentRepository: IConsignmentRepository
	{
		private WarehouseEntities _context;

		public ConsignmentRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public List<Consignment> GetListOfConsignments()
		{
			return _context.Consignments.ToList();
		}
	}
}
