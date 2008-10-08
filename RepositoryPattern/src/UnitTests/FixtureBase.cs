using FluentNHibernate;
using FluentNHibernate.Framework;
using NHibernate;
using NUnit.Framework;
using RepositoryPattern;

namespace UnitTests
{
    public class FixtureBase
    {
        protected SessionSource SessionSource { get; set; }
        protected ISession Session { get; private set; }

        [SetUp]
        public void SetupContext()
        {
            Before_each_test();
        }

        [TearDown]
        public void TearDownContext()
        {
            After_each_test();
        }

        protected virtual void Before_each_test()
        {
            SessionSource = new SessionSource(new TestModel());
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);
            CreateInitialData(Session);
            Session.Flush();
            Session.Clear();
        }

        protected virtual void After_each_test()
        {
            Session.Close();
            Session.Dispose();
        }

        protected virtual void CreateInitialData(ISession session)
        {
        }
    }

    public class TestModel : PersistenceModel
    {
        public TestModel()
        {
            addMappingsFromAssembly(typeof(ProductMap).Assembly);
        }
    }
}