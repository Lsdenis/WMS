namespace WMS.BusinessLogic.PickAndStoreObjects
{
	public class PickObject
	{
		public int GoodId;
		public int? CellId;
		public int UserId;
		public int Count;

		public PickObject(int goodId, int? cellId, int userId, int count)
		{
			GoodId = goodId;
			CellId = cellId;
			UserId = userId;
			Count = count;
		}

		public PickObject()
		{
			
		}
	}
}
