using System;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.ListViewItems
{
	public class LVGoodItem
	{
		public string Name { get; set; }
		public string Consignment { get; set; }
		public DateTime AddingDate { get; set; }
		public int Count { get; set; }
		public int Id { get; set; }
		public string AddingDateString
		{
			get
			{
				return AddingDate.ToShortDateString();
			}
		}

		public LVGoodItem(Good good)
		{
			Name = good.Name;
			if (good.Consignment != null)
			{
				Consignment = good.Consignment.Name;
			}

			AddingDate = good.AddingDate;

			Count = good.Count;

			Id = good.Id;
		}

		public LVGoodItem()
		{

		}
	}
}
