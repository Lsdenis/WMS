using System.Collections.Generic;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	public class GoodsInCellsRepository : IGoodsInCellsRepository
	{
		private WarehouseEntities _context;

		public GoodsInCellsRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public List<GoodsInCell> GetListOfGoodsInCells()
		{
			return _context.GoodsInCells.ToList();
		}

		public void AddGoodInCell(Good good, Cell cell)
		{
			throw new System.NotImplementedException();
		}

		public void UpdateGoodInCell(Good good, Cell cell)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteGoodInCell(Good good, Cell cell)
		{
			throw new System.NotImplementedException();
		}

		public void DeleteGoodInCells(Good good)
		{
			var goodInCells = _context.GoodsInCells.Where(gic => gic.GoodId == good.Id).ToList();

			_context.GoodsInCells.RemoveRange(goodInCells);
		}
	}
}
