using FluentNHibernate;
using FluentNHibernate.Framework;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class FluentInterfaceFixtureBase<TModel>  where TModel : PersistenceModel, new()
    {
        protected SessionSource SessionSource { get; set; }
        protected ISession Session { get; private set; }

        [SetUp]
        public void SetupContext()
        {
            Context();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownContext();
        }

        protected virtual void Context()
        {
            SessionSource = new SessionSource(new TModel());
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);
            CreateInitialData(Session);
            Session.Flush();
            Session.Clear();
        }

        protected virtual void TearDownContext()
        {
            Session.Close();
            Session.Dispose();
        }

        protected virtual void CreateInitialData(ISession session)
        {
        }
    }

}