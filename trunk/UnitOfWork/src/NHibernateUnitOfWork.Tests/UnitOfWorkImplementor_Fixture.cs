using System.Data;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace NHibernateUnitOfWork.Tests
{
    [TestFixture]
    public class UnitOfWorkImplementor_Fixture
    {
        private readonly MockRepository _mocks = new MockRepository();
        private IUnitOfWorkFactory _factory;
        private ISession _session;
        private IUnitOfWorkImplementor _uow;

        [SetUp]
        public void SetupContext()
        {
            _factory = _mocks.DynamicMock<IUnitOfWorkFactory>();
            _session = _mocks.DynamicMock<ISession>();
        }

        [Test]
        public void Can_create_UnitOfWorkImplementor()
        {
            using(_mocks.Record())
            {
                
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, _session);
                Assert.AreSame(_factory, ((UnitOfWorkImplementor)_uow).Factory);
                Assert.AreSame(_session, ((UnitOfWorkImplementor)_uow).Session);
            }
        }

        [Test]
        public void Can_Dispose_UnitOfWorkImplementor()
        {
            using(_mocks.Record())
            {
                Expect.Call(() => _factory.DisposeUnitOfWork(null)).IgnoreArguments();
                Expect.Call(_session.Dispose);
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, _session);
                _uow.Dispose();
            }
        }

        [Test]
        public void Can_Flush_UnitOfWorkImplementor()
        {
            using(_mocks.Record())
            {
                Expect.Call(_session.Flush);
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, _session);
                _uow.Flush();
            }
        }

        [Test]
        public void Can_BeginTransaction()
        {
            using(_mocks.Record())
            {
                Expect.Call(_session.BeginTransaction()).Return(null);
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, _session);
                var transaction = _uow.BeginTransaction();
                Assert.IsNotNull(transaction);
            }
        }

        [Test]
        public void Can_BeginTransaction_specifying_isolation_level()
        {
            var isolationLevel = IsolationLevel.Serializable;
            using(_mocks.Record())
            {   
                Expect.Call(_session.BeginTransaction(isolationLevel)).Return(null);
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, _session);
                var transaction = _uow.BeginTransaction(isolationLevel);
                Assert.IsNotNull(transaction);
            }
        }

        [Test]
        public void Can_execute_TransactionalFlush()
        {
            var tx = _mocks.CreateMock<ITransaction>();
            var session = _mocks.DynamicMock<ISession>();
            SetupResult.For(session.BeginTransaction(IsolationLevel.ReadCommitted)).Return(tx);

            _uow = _mocks.PartialMock<UnitOfWorkImplementor>(_factory, _session);

            using (_mocks.Record())
            {
                Expect.Call(tx.Commit);
                Expect.Call(tx.Dispose);
            }
            using (_mocks.Playback())
            {
                _uow = new UnitOfWorkImplementor(_factory, session);
                _uow.TransactionalFlush();
            }
        }

        [Test]
        public void Can_execute_TransactionalFlush_specifying_isolation_level()
        {
            var tx = _mocks.CreateMock<ITransaction>();
            var session = _mocks.DynamicMock<ISession>();
            SetupResult.For(session.BeginTransaction(IsolationLevel.Serializable)).Return(tx);

            _uow = _mocks.PartialMock<UnitOfWorkImplementor>(_factory, session);

            using (_mocks.Record())
            {
                Expect.Call(tx.Commit);
                Expect.Call(tx.Dispose);
            }
            using (_mocks.Playback())
            {
                _uow.TransactionalFlush(IsolationLevel.Serializable);
            }
        }
    }
}