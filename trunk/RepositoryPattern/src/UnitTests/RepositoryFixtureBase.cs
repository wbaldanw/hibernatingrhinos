using NHibernate.Tool.hbm2ddl;
using NHibernateUnitOfWork;
using NUnit.Framework;

namespace UnitTests
{
    public class RepositoryFixtureBase<T>
    {
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            UnitOfWork.Configuration.AddAssembly(typeof(T).Assembly);
        }

        [SetUp]
        public void SetupContext()
        {
            UnitOfWork.Start();

            new SchemaExport(UnitOfWork.Configuration)
                .Execute(false, true, false, false, UnitOfWork.CurrentSession.Connection, null);

            Context();
        }

        [TearDown]
        public void TearDownContext()
        {
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.Current.Dispose();
        }

        protected virtual void Context() { }
    }
}