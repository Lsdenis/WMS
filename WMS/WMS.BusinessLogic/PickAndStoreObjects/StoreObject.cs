namespace WMS.BusinessLogic.PickAndStoreObjects
{
	public class StoreObject
	{
		public int CellId;
		public int CardId;
		public int Count;

		public StoreObject(int cardId, int cellId, int count)
		{
			CellId = cellId;
			CardId = cardId;
			Count = count;
		}

		public StoreObject()
		{
			
		}
	}
}
