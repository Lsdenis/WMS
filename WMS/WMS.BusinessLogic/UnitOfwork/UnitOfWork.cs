using System;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Classes;
using WMS.BusinessLogic.Repository.Interfaces;
using WMS.BusinessLogic.Services.Classes;
using WMS.BusinessLogic.Services.Interfaces;

namespace WMS.BusinessLogic.UnitOfwork
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly WarehouseEntities _context;
		private bool _disposed;

		#region Repositories
		private IUserRepository _userRepository;
		private IGoodsRepository _goodsRepository;
		private IConsignmentRepository _consignmentRepository;
		private IGoodsInCellsRepository _goodsInCellsRepository;
		private ICellsRepository _cellRepository;
		private IWarehouseRepository _warehouseRepository;
		private IPickAndStoreService _pickAndStoreService;
		private IPickAndStoreValidationService _pickAndStoreValidationService;
		private IUserCartsRepository _userCartsRepository;
		#endregion

		#region Properties
		public IUserRepository Users
		{
			get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
		}
		public IGoodsRepository Goods
		{
			get { return _goodsRepository ?? (_goodsRepository = new GoodsRepository(_context)); }
		}
		public IConsignmentRepository Consignments
		{
			get { return _consignmentRepository ?? (_consignmentRepository = new ConsignmentRepository(_context)); }
		}
		public IGoodsInCellsRepository GoodsInCells
		{
			get { return _goodsInCellsRepository ?? (_goodsInCellsRepository = new GoodsInCellsRepository(_context)); }
		}
		public ICellsRepository Cells
		{
			get { return _cellRepository ?? (_cellRepository = new CellRepository(_context)); }
		}
		public IWarehouseRepository Warehouses
		{
			get { return _warehouseRepository ?? (_warehouseRepository = new WarehouseRepository(_context)); }
		}
		public IPickAndStoreService PickAndStoreService
		{
			get { return _pickAndStoreService ?? (_pickAndStoreService = new PickAndStoreService(_context)); }
		}
		public IPickAndStoreValidationService PickAndStoreValidationService
		{
			get { return _pickAndStoreValidationService ?? (_pickAndStoreValidationService = new PickAndStoreValidationService(_context)); }
		}
		public IUserCartsRepository UserCartsRepository
		{
			get { return _userCartsRepository ?? (_userCartsRepository = new UserCartsRepository(_context)); }
		}
		#endregion

		public UnitOfWork()
		{
			_context = new WarehouseEntities();
		}
		public void Commit()
		{
			_context.SaveChanges();
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_context.Dispose();
				}
			}
			_disposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
