using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Framework;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace TreeStructure.Tests
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
            Session.Flush();
            Session.Clear();

            Because();
        }

        protected void ShowSchema()
        {
            var cfg = new SQLiteConfiguration()
                .InMemory()
                .ShowSql();
            var configuration = new Configuration().AddProperties(cfg.ToProperties());
            var model = new TModel();
            model.Configure(configuration);
            new SchemaExport(configuration).Create(true, false);
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