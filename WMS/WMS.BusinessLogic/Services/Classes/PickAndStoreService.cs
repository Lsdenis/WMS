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

		public void PickGood(PickObject pickObject)
		{
			var userCart = new UserCart();
			userCart.GoodId = pickObject.GoodId;
			userCart.UserId = pickObject.UserId;
			userCart.Count = pickObject.Count;

			if (pickObject.CellId != null)
			{
				DeleteFromCell(pickObject);
			}

			var existingUserCart =
				_context.UserCarts.SingleOrDefault(
					uc => uc.GoodId == pickObject.GoodId && uc.UserId == pickObject.UserId);
			if (existingUserCart == null)
			{
				_context.UserCarts.Add(userCart);
			}
			else
			{
				existingUserCart.Count += pickObject.Count;
			}
		}

		private void DeleteFromCell(PickObject pickObject)
		{
			var selectedGood =
				_context.GoodsInCells.SingleOrDefault(
					gic => gic.CellId == pickObject.CellId && gic.GoodId == pickObject.GoodId);

			if (selectedGood == null)
			{
				return;
			}

			if (selectedGood.Value == pickObject.Count)
			{
				_context.GoodsInCells.Remove(selectedGood);
			}
			else
			{
				selectedGood.Value -= pickObject.Count;
			}
		}

		public void StoreGood(StoreObject storeObject)
		{
			var selectedCard = _context.UserCarts.SingleOrDefault(uc => uc.Id == storeObject.CardId);
			if (selectedCard == null)
			{
				return;
			}

			var goodInCell = new GoodsInCell();
			goodInCell.CellId = storeObject.CellId;
			goodInCell.GoodId = selectedCard.GoodId;
			goodInCell.Value = storeObject.Count;

			AddToCell(goodInCell);

			if (selectedCard.Count == storeObject.Count)
			{
				_context.UserCarts.Remove(selectedCard);
			}
			else
			{
				selectedCard.Count -= storeObject.Count;
			}
		}

		public void ShipGood(int userCartId)
		{
			var userCart = _context.UserCarts.SingleOrDefault(gd => gd.Id == userCartId);
			if (userCart == null)
			{
				return;
			}

			var good = _context.Goods.Single(gd => gd.Id == userCart.GoodId);
			_context.UserCarts.Remove(userCart);

			if (userCart.Count == good.Count)
			{
				_context.Goods.Remove(good);
			}
			else
			{
				good.Count -= userCart.Count;
			}

		}

		private void AddToCell(GoodsInCell goodInCell)
		{
			var existingGIC =
				_context.GoodsInCells.SingleOrDefault(gic => gic.CellId == goodInCell.CellId && gic.GoodId == goodInCell.GoodId);

			if (existingGIC == null)
			{
				_context.GoodsInCells.Add(goodInCell);
			}
			else
			{
				existingGIC.Value += goodInCell.Value;
			}
		}
	}
}
