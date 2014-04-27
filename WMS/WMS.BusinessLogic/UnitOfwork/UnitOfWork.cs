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
		#endregion

		#region Properties
		public IUserRepository Users
		{
			get
			{
				if (_userRepository == null)
				{
					_userRepository = new UserRepository(_context);
				}
				return _userRepository;
			}
		}
		#endregion

		public UnitOfWork()
		{
			_context = new WarehouseEntities();
		}

		public void Save()
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
			//magic?
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
