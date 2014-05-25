namespace WMS.BusinessLogic.PickAndStoreObjects
{
	public class PickAndStoreObject
	{
		public int GoodId;
		public int? CellId;
		public int UserId;
		public int Count;

		public PickAndStoreObject(int goodId, int? cellId, int userId, int count)
		{
			GoodId = goodId;
			CellId = cellId;
			UserId = userId;
			Count = count;
		}

		public PickAndStoreObject()
		{
			
		}
	}
}
