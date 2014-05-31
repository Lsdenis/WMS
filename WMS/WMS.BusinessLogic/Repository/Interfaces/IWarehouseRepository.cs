using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IWarehouseRepository
	{
		void AddWarehouse(Warehouse warehouse);
		void UpdateWarehouse(Warehouse warehouse);
		void DeleteWarehouses(Warehouse warehouse);
		Warehouse GetWarehouse(int id);
		IEnumerable<Warehouse> GetWarehouses();
	}
}
