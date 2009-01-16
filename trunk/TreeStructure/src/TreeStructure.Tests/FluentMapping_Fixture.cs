using FluentNHibernate;
using NUnit.Framework;
using System.Linq;

namespace TreeStructure.Tests
{
    public class MyModel : PersistenceModel
    {
        public MyModel()
        {
            addMappingsFromAssembly(typeof(NodeMap).Assembly);
        }
    }

    [TestFixture]
    public class FluentMapping_Fixture : FixtureBase<MyModel>
    {
        [Test]
        public void test_mapping_of_node_tree()
        {
            ShowSchema();
        }

        [Test]
        public void can_add_node()
        {
            var node = new Node{Name="root"};
            var id = Session.Save(node);

            Session.Flush();
            Session.Clear();

            Session.Get<Node>(id);
        }

        [Test]
        public void can_add_node_with_child()
        {
            var node = new Node { Name = "root" };
            var child = new Node {Name = "Child"};
            node.AddChild(child);

            var id = Session.Save(node);

            Session.Flush();
            Session.Clear();

            var fromDb = Session.Get<Node>(id);

            Assert.IsNull(fromDb.Parent);
            Assert.AreEqual(1, fromDb.Children.Count);
            Assert.AreEqual(1, fromDb.Descendants.Count);
            Assert.AreEqual(0, fromDb.Ancestors.Count);

            var first = fromDb.Children.First();
            Assert.AreEqual(1, first.Ancestors.Count);
            Assert.AreSame(fromDb, first.Parent);
        }
    }
}