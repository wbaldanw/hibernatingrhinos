using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Framework;
using NHibernate;
using NUnit.Framework;

namespace UnitTests
{
    public class FixtureBase<TModel> where TModel : PersistenceModel, new()
    {
        protected SessionSource SessionSource { get; set; }
        protected ISession Session { get; private set; }

        [SetUp]
        public void SetupContext()
        {
            var cfg = new SQLiteConfiguration()
                            .InMemory()
                            .ShowSql();
            SessionSource = new SessionSource(cfg.ToProperties(), new TModel());
            Session = SessionSource.CreateSession();
            SessionSource.BuildSchema(Session);
            
            Context();
            Because();

            Session.Flush();
            Session.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            TearDownContext();

            Session.Close();
            Session.Dispose();
        }

        protected virtual void Context()
        {
        }

        protected virtual void Because()
        {
        }

        protected virtual void TearDownContext()
        {
        }
    }
}