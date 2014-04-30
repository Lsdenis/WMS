using System;
using WMS.BusinessLogic.DataModel;
using WMS.BusinessLogic.Repository.Classes;
using WMS.BusinessLogic.Repository.Interfaces;

namespace WMS.BusinessLogic.UnitOfwork
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly WarehouseEntities _context;
		private bool _disposed;

		#region Repositories

		private IUserRepository _userRepository;
		private IGoodsRepository _goodsRepository;
		#endregion

		#region Properties
		public IUserRepository Users
		{
			get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
		}

		public IGoodsRepository Goods
		{
			get
			{
				return _goodsRepository ?? (_goodsRepository = new GoodsRepository(_context));
			}
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
