using System.Collections.Generic;
using NUnit.Framework;

namespace CollectionMapping.Tests
{
    [TestFixture]
    public class When_no_drive_system_exists : Fixture_Base
    {
        [Test]
        public void Can_create_a_drive_system_with_distinct_equipment()
        {
            var ds = new DriveSystem { Name = "Drive System 1" };
            ds.Equipments.Add("Motor");
            ds.Equipments.Add("Filter");
            ds.Equipments.Add("Transformer");
            ds.Equipments.Add("Starter");

            Session.Save(ds);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<DriveSystem>(ds.Id);
            Assert.AreNotSame(ds, fromDb);
            Assert.AreEqual(ds.Name, fromDb.Name);
            Assert.AreEqual(ds.Equipments.Count, fromDb.Equipments.Count);
            // Assert that sort is working
            Assert.AreEqual("Filter", fromDb.Equipments[0]);
        }

        [Test]
        public void Can_create_a_drive_system_containing_same_equipment_more_than_once()
        {
            var ds = new DriveSystem { Name = "Drive System 1" };
            ds.Equipments.Add("Motor");
            ds.Equipments.Add("Motor");
            ds.Equipments.Add("Motor");
            ds.Equipments.Add("Filter");
            ds.Equipments.Add("Transformer");
            ds.Equipments.Add("Starter");

            Session.Save(ds);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<DriveSystem>(ds.Id);
            Assert.AreNotSame(ds, fromDb);
            Assert.AreEqual(ds.Name, fromDb.Name);
            Assert.AreEqual(ds.Equipments.Count, fromDb.Equipments.Count);
            // Assert that bag can really contain same equipment multiple times
            var temp = new List<string>(fromDb.Equipments);
            Assert.AreEqual(3, temp.FindAll(e => e == "Motor").Count);
        }

        [Test]
        public void Can_create_a_drive_system_containing_same_manual_more_than_once()
        {
            var ds = new DriveSystem {Name = "Drive System 1"};
            var englishManual = new Manual {Title = "Operation Manual", Language = "English"};
            ds.Manuals.Add(englishManual);
            ds.Manuals.Add(englishManual);
            ds.Manuals.Add(new Manual {Title = "Operation Manual", Language = "German"});

            Session.Save(ds);
            Session.Flush();
            Session.Clear();

            // Assertions
            var fromDb = Session.Get<DriveSystem>(ds.Id);
            Assert.AreEqual(ds.Manuals.Count, fromDb.Manuals.Count);
            // Assert that bag can really contain same equipment multiple times
            var temp = new List<Manual>(fromDb.Manuals);
            Assert.AreEqual(2, temp.FindAll(m => m.Title == englishManual.Title && 
                                                 m.Language == englishManual.Language).Count);
        }
    }
}