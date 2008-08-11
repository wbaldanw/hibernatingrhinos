using FluentMapping.Infrastructure.Mappings;
using FluentNHibernate;
using NHibernate;
using NUnit.Framework;

namespace FluentMapping.UnitTests
{
    public class FixtureBase
    {
        protected MySessionSource SessionSource { get; set; }
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
            SessionSource = new MySessionSource(new TestModel());
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);
            CreateInitialData(Session);
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