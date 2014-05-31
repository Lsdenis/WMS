using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.ListViewItems;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.Repository.Classes
{
	public class GoodsRepository: IGoodsRepository
	{
		private readonly WarehouseEntities _context;

		public GoodsRepository(WarehouseEntities context)
		{
			_context = context;
		}

		public List<Good> GetAllGoods()
		{
			return _context.Goods.ToList();
		}

		public void AddGood(Good good)
		{
			_context.Goods.Add(good);
		}

		public void UpdateGood(Good good)
		{
			var existingGood = _context.Goods.FirstOrDefault(gd => gd.Id == good.Id);
			if (existingGood == null)
			{
				return;
			}
			_context.Entry(existingGood).CurrentValues.SetValues(good);
			_context.Entry(existingGood).State = EntityState.Modified;
		}

		public void DeleteGood(Good good)
		{
			var existingGood = _context.Goods.FirstOrDefault(gd => gd.Id == good.Id);
			if (existingGood == null)
			{
				return;
			}
			_context.Goods.Remove(existingGood);
		}

		public List<LVGoodItem> GetLVGoods()
		{
			var lvgoods = _context.Goods.Select(good => new LVGoodItem
			{
				AddingDate = good.AddingDate,
				Consignment = good.Consignment.Name,
				Count = good.Count,
				Id = good.Id,
				Name = good.Name
			}).ToList();
			return lvgoods;
		}
	}
}
