using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace TreeStructure.Tests
{
    [TestFixture]
    public class EquipmentRepository_Fixture
    {
        private ISessionFactory _sessionFactory;
        private EquipmentRepository _repository;
        private Equipment _rootEquipment, _childEquipment1, _childEquipment2;
        private Equipment _grandCildEquipment1_1, _grandCildEquipment1_2, _grandCildEquipment1_3;
        private Equipment _grandCildEquipment2_1, _grandCildEquipment2_2;

        [SetUp]
        public void SetupContext()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof (Equipment).Assembly);
            new SchemaExport(configuration).Execute(false, true, false, false);

            _sessionFactory = configuration.BuildSessionFactory();

            CreateInitialData();

            _repository = new EquipmentRepository();
        }

        private void CreateInitialData()
        {
            _rootEquipment = new Equipment {Name = "Motor"};
            _childEquipment1 = new Equipment {Name = "AC Motor"};
            _childEquipment2 = new Equipment {Name = "DC Motor"};
            _grandCildEquipment1_1 = new Equipment {Name = "AC LV Motor"};
            _grandCildEquipment1_2 = new Equipment {Name = "AC MV Motor"};
            _grandCildEquipment1_3 = new Equipment {Name = "AC HV Motor"};
            _grandCildEquipment2_1 = new Equipment {Name = "DC LV Motor"};
            _grandCildEquipment2_2 = new Equipment {Name = "DC MV Motor"};

            _rootEquipment.AddChildEquipment(_childEquipment1);
            _rootEquipment.AddChildEquipment(_childEquipment2);
            _childEquipment1.AddChildEquipment(_grandCildEquipment1_1);
            _childEquipment1.AddChildEquipment(_grandCildEquipment1_2);
            _childEquipment1.AddChildEquipment(_grandCildEquipment1_3);
            _childEquipment2.AddChildEquipment(_grandCildEquipment2_1);
            _childEquipment2.AddChildEquipment(_grandCildEquipment2_2);

            using (var session = _sessionFactory.OpenSession())
            {
                session.Save(_rootEquipment);
                session.Flush();
            }
        }

        [Test]
        public void Can_load_aggregate_by_id()
        {
            var equipmentId = _childEquipment1.Id;
            var equipment = _repository.GetAggregateById(equipmentId);
            Assert.IsNotNull(equipment);
            Assert.IsTrue(NHibernateUtil.IsInitialized(equipment.Parent));
            Assert.IsTrue(NHibernateUtil.IsInitialized(equipment.Children));
            Assert.AreEqual(3, equipment.Children.Count);
        }

        [Test]
        public void Can_load_root_aggregate()
        {
            var equipment = _repository.GetRootAggregate();
            Assert.IsNotNull(equipment);
            Assert.AreEqual(_rootEquipment.Name, equipment.Name);
            Assert.IsNull(equipment.Parent);
            Assert.IsTrue(NHibernateUtil.IsInitialized(equipment.Children));
            Assert.AreEqual(2, equipment.Children.Count);
        }

        [Test]
        public void Can_load_all_leaf_nodes()
        {
            var leafs = _repository.GetAllLeafNodes();
            Assert.AreEqual(5, leafs.Count);
        }

        [Test]
        public void Can_load_descendants()
        {
            var descendants = _repository.GetAllDescendantsOfEquipment(_rootEquipment.Id);
            Assert.AreEqual(7, descendants.Count);
        }

        [Test]
        public void Can_load_ancestors()
        {
            var ancestors = _repository.GetAllAncestorsOfEquipment(_grandCildEquipment1_1.Id);
            Assert.AreEqual(2, ancestors.Count);
        }
    }
}