using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using NHibernateUnitOfWork;
using NUnit.Framework;

namespace NHibernateRepository.Tests
{
    [TestFixture]
    public class Repository_Fixture
    {
        private IUnitOfWork _unitOfWork;
        private Product _product;
        private IRepository<Product> _repository;

        private Configuration Configuration
        {
            get { return UnitOfWork.Configuration; }
        }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Configuration.AddAssembly(typeof (Product).Assembly);
        }

        [SetUp]
        public void SetupContext()
        {
            _unitOfWork = UnitOfWork.Start();
            new SchemaExport(Configuration).Execute(false, true, false, false, UnitOfWork.CurrentSession.Connection, null);

            CreateInitialData();
            _repository = new Repository<Product>();
        }

        private void CreateInitialData()
        {
            _product = new Product{Name = "Apples"};
            UnitOfWork.CurrentSession.Save(_product);
            UnitOfWork.Current.TransactionalFlush();
            UnitOfWork.CurrentSession.Clear();
        }

        [TearDown]
        public void TearDownContext()
        {
            _unitOfWork.Dispose();
        }

        [Test]
        public void Can_get_product()
        {
            var fromDb = _repository.Get(_product.Id);
            Assert.IsNotNull(fromDb);
        }

        [Test]
        public void Can_find_all()
        {
            var products = _repository.FindAll(Order.Asc("Name"));
            Assert.AreEqual(1, products.Count);
        }

        [Test]
        public void Can_report_product()
        {
            var products = _repository.ReportAll<ProductDTO>(Projections.ProjectionList()
                                                            .Add(Projections.Property("Id"))
                                                            .Add(Projections.Property("Name")));
            Assert.AreEqual(1, products.Count);
        }
    }
}
