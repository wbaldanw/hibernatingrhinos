using System;
using NHibernate;
using NHibernate.Cfg;

namespace NHibernateUnitOfWork
{
    public static class UnitOfWork
    {
        private static IUnitOfWorkFactory _unitOfWorkFactory = new UnitOfWorkFactory();
        private static IUnitOfWork _innerUnitOfWork;

        public static Configuration Configuration
        {
            get { return _unitOfWorkFactory.Configuration; }
        }

        public static IUnitOfWork Current
        {
            get
            {
                if (_innerUnitOfWork == null)
                    throw new InvalidOperationException("You are not in a unit of work.");
                return _innerUnitOfWork;
            }
        }

        public static bool IsStarted
        {
            get { return _innerUnitOfWork != null; }
        }

        public static ISession CurrentSession
        {
            get { return _unitOfWorkFactory.CurrentSession; }
            internal set { _unitOfWorkFactory.CurrentSession = value; }
        }

        public static IUnitOfWork Start()
        {
            if (_innerUnitOfWork != null)
                throw new InvalidOperationException("You cannot start more than one unit of work at the same time.");
            
            _innerUnitOfWork = _unitOfWorkFactory.Create();
            return _innerUnitOfWork;
        }

        public static void DisposeUnitOfWork(IUnitOfWorkImplementor unitOfWork)
        {
            _innerUnitOfWork = null;
        }
    }
}
