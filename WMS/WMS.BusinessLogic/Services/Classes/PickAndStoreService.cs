using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.PickAndStoreObjects;
using WMS.BusinessLogic.Services.Interfaces;

namespace WMS.BusinessLogic.Services.Classes
{
	public class PickAndStoreService : IPickAndStoreService
	{
		private readonly WarehouseEntities _context;

		public PickAndStoreService(WarehouseEntities context)
		{
			_context = context;
		}

		public void PickGood(PickAndStoreObject pickAndStoreObject)
		{
			var userCart = new UserCart();
			userCart.GoodId = pickAndStoreObject.GoodId;
			userCart.UserId = pickAndStoreObject.UserId;
			userCart.Count = pickAndStoreObject.Count;

			if (pickAndStoreObject.CellId != null)
			{
				DeleteFromCell(pickAndStoreObject);
			}

			_context.UserCarts.Add(userCart);
		}

		private void DeleteFromCell(PickAndStoreObject pickAndStoreObject)
		{
			var selectedGood =
				_context.GoodsInCells.SingleOrDefault(
					gic => gic.CellId == pickAndStoreObject.CellId && gic.GoodId == pickAndStoreObject.GoodId);

			if (selectedGood == null)
			{
				return;
			}

			if (selectedGood.Value == pickAndStoreObject.Count)
			{
				_context.GoodsInCells.Remove(selectedGood);
			}
			else
			{
				selectedGood.Value -= pickAndStoreObject.Count;
			}
		}

		public void StoreGood(PickAndStoreObject pickAndStoreObject)
		{
			if (pickAndStoreObject.CellId == null)
			{
				return;
			}

			var goodInCell = new GoodsInCell();
			goodInCell.CellId = pickAndStoreObject.CellId.Value;
			goodInCell.GoodId = pickAndStoreObject.GoodId;
			goodInCell.Value = pickAndStoreObject.Count;

			DeleteFromUserCart(pickAndStoreObject);

			_context.GoodsInCells.Add(goodInCell);
		}

		private void DeleteFromUserCart(PickAndStoreObject pickAndStoreObject)
		{
			var currentUserCart =
				_context.UserCarts.FirstOrDefault(
					uc => uc.GoodId == pickAndStoreObject.GoodId && uc.UserId == pickAndStoreObject.UserId);

			if (currentUserCart == null)
			{
				return;
			}

			if (pickAndStoreObject.Count == currentUserCart.Count)
			{
				_context.UserCarts.Remove(currentUserCart);
			}
			else
			{
				currentUserCart.Count -= pickAndStoreObject.Count;
			}
		}
	}
}
