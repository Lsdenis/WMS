using System.Collections.Generic;
using WMS.BusinessLogic.DataModel;

namespace WMS.BusinessLogic.Repository.Interfaces
{
	public interface IConsignmentRepository
	{
		List<Consignment> GetListOfConsignments();
	}
}
