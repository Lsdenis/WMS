using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	public class CellRepository: ICellsRepository
	{
		private WarehouseEntities _context;

		public CellRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public Cell GetCell(int id)
		{
			return _context.Cells.SingleOrDefault(cell => cell.Id == id);
		}

		public IEnumerable<Cell> GetCells()
		{
			return _context.Cells.ToList();
		}

		public IEnumerable<LVCellsItems> GetLVCells()
		{
			return _context.Cells.Select(cell => new LVCellsItems
			{
				Id = cell.Id,
				MaxValue = cell.MaxValue,
				Number = cell.Number,
				Warehouse = cell.Warehouse.Name,
				Value = cell.GoodsInCells.Count
			}).ToList();
		}

		public void AddCell(Cell cell)
		{
			_context.Cells.Add(cell);
		}

		public void DeleteCell(Cell cell)
		{
			var existingCell = _context.Cells.SingleOrDefault(cl => cl.Id == cell.Id);
			if (existingCell == null)
			{
				return;
			}

			_context.Cells.Remove(existingCell);
		}

		public void UpdateCell(Cell cell)
		{
			var existingCell = _context.Cells.SingleOrDefault(cl => cl.Id == cell.Id);
			if (existingCell == null)
			{
				return;
			}

			_context.Entry(existingCell).CurrentValues.SetValues(cell);
			_context.Entry(existingCell).State = EntityState.Modified;
		}
	}
}
