using System.Dynamic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.ListViewItems
{
	public class LVCellsItems
	{
		public int Number { get; set; }
		public string Warehouse { get; set; }
		public int Value { get; set; }
		public int MaxValue { get; set; }
		public int Id { get; set; }

		public LVCellsItems()
		{
			
		}

		public LVCellsItems(Cell cell)
		{
			Number = cell.Number;

			if (cell.Warehouse != null)
			{
				Warehouse = cell.Warehouse.Name;
			}

			Value = cell.GoodsInCells.Count;

			MaxValue = cell.MaxValue;

			Id = cell.Id;
		}
	}
}
