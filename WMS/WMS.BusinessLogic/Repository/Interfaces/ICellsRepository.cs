using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface ICellsRepository
	{
		Cell GetCell(int id);
		IEnumerable<Cell> GetCells();
		IEnumerable<LVCellsItems> GetLVCells();
		void AddCell(Cell cell);
		void DeleteCell(Cell cell);
		void UpdateCell(Cell cell);
	}
}
