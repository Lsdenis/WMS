using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IGoodsInCellsRepository
	{
		List<GoodsInCell> GetListOfGoodsInCells();
		void AddGoodInCell(Good good, Cell cell);
		void UpdateGoodInCell(Good good, Cell cell);
		void DeleteGoodInCell(Good good, Cell cell);
		void DeleteGoodInCells(Good good);
	}
}
