using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	class WarehouseRepository : IWarehouseRepository
	{
		private WarehouseEntities _context;

		public WarehouseRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public void AddWarehouse(Warehouse warehouse)
		{
			_context.Warehouses.Add(warehouse);
		}

		public void UpdateWarehouse(Warehouse warehouse)
		{
			var currentWarehouse = _context.Warehouses.SingleOrDefault(whs => whs.Id == warehouse.Id);
			if (currentWarehouse == null)
			{
				return;
			}

			_context.Entry(currentWarehouse).CurrentValues.SetValues(warehouse);
			_context.Entry(currentWarehouse).State = EntityState.Modified;
		}

		public void DeleteWarehouses(Warehouse warehouse)
		{
			var currentWarehouse = _context.Warehouses.SingleOrDefault(whs => whs.Id == warehouse.Id);
			if (currentWarehouse == null)
			{
				return;
			}

			_context.Warehouses.Remove(currentWarehouse);
		}

		public Warehouse GetWarehouse(int id)
		{
			return _context.Warehouses.SingleOrDefault(whs => whs.Id == id);
		}

		public IEnumerable<Warehouse> GetWarehouses()
		{
			return _context.Warehouses.ToList();
		}
	}
}
