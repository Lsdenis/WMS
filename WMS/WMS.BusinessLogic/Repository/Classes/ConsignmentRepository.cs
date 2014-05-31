using System.Collections.Generic;
using System.Data.Entity;
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

		public void Add(Consignment consignment)
		{
			_context.Consignments.Add(consignment);
		}

		public void Update(Consignment consignment)
		{
			var currentConsignment = _context.Consignments.SingleOrDefault(cg => cg.Id == consignment.Id);
			if (currentConsignment == null)
			{
				return;
			}

			_context.Entry(currentConsignment).CurrentValues.SetValues(consignment);
			_context.Entry(currentConsignment).State = EntityState.Modified;
		}

		public void Delete(Consignment consignment)
		{
			var currentConsignment = _context.Consignments.SingleOrDefault(cg => cg.Id == consignment.Id);
			if (currentConsignment == null)
			{
				return;
			}

			_context.Consignments.Remove(currentConsignment);
		}
	}
}
