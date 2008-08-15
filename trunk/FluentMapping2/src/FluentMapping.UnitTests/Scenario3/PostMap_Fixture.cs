using System;
using System.Xml;
using FluentMapping.Infrastructure.Mappings.Scenario3;
using FluentNHibernate;
using NUnit.Framework;

namespace FluentMapping.UnitTests.Scenario3
{
    [TestFixture]
    public class PostMap_Fixture
    {
        private PostMap _classMap;

        [SetUp]
        public void SetupContext()
        {
            _classMap = new PostMap();
        }

        [Test]
        public void Can_generate_xml_from_mapping_class()
        {
            var document = _classMap.CreateMapping(new MappingVisitor());
            Console.WriteLine(document.OuterXml);
        }

        [Test]
        public void Should_specify_collection_as_set()
        {
            var document = _classMap.CreateMapping(new MappingVisitor());
            var classElement = (XmlElement)document.DocumentElement.SelectSingleNode("class");
            classElement.ShouldHaveChild("set");
        }

        [Test]
        public void Should_specify_that_collection_contains_composite_element()
        {
            var document = _classMap.CreateMapping(new MappingVisitor());
            var classElement = (XmlElement)document.DocumentElement.SelectSingleNode("class/set");
            classElement.ShouldHaveChild("composite-element");
        }

        [Test]
        public void Should_map_children_of_composite_element_correctly()
        {
            var document = _classMap.CreateMapping(new MappingVisitor());
            TestMappedProperty(document, "Text", "String");
            TestMappedProperty(document, "AuthorEmail", "String");
            TestMappedProperty(document, "CreationDate", "DateTime");
        }

        private void TestMappedProperty(XmlDocument document, string name, string type)
        {
            var filter = string.Format("//composite-element/property[@name='{0}']", name);
            var classElement = (XmlElement)document.DocumentElement.SelectSingleNode(filter);
            classElement.ShouldNotBeNull();
            classElement.Attributes["type"].Value.ShouldEqual(type);
        }
    }
}