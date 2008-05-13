using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Cfg;

namespace TreeStructure
{
    public class SessionManager : ISessionManager
    {
        private static SessionManager _instance;
        private readonly ISessionFactory _sessionFactory;

        private SessionManager()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddAssembly(typeof(Equipment).Assembly);
            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISessionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SessionManager();
                return _instance;
            }
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }

    public interface ISessionManager
    {
        ISession OpenSession();
    }

    public class EquipmentRepository
    {
        public ISessionManager SessionManager { get; set; }

        public EquipmentRepository()
        {
            SessionManager = TreeStructure.SessionManager.Instance;
        }

        public Equipment GetAggregateById(Guid equipmentId)
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "from Equipment e" +
                          " left join fetch e.Parent p" +
                          " left join fetch e.Children c" +
                          " where e.Id = :id";
                var equipment = session.CreateQuery(sql)
                    .SetGuid("id", equipmentId)
                    .UniqueResult<Equipment>();
                return equipment;
            }
        }

        public Equipment GetRootAggregate()
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "from Equipment e" +
                          " left join fetch e.Children c" +
                          " where e.Parent is null";
                var equipment = session.CreateQuery(sql)
                    .UniqueResult<Equipment>();
                return equipment;
            }
        }

        public ICollection<Equipment> GetAllLeafNodes()
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "from Equipment e" +
                          " left join e.Parent p" +
                          " where e.Children.size = 0";
                var leafs = session.CreateQuery(sql)
                    .List<Equipment>();
                return leafs;
            }
        }

        public ICollection<Equipment> GetAllDescendantsOfEquipment(Guid equipmentId)
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "with Hierachy(Id, Name, ParentId, Level)" +
                          " as" +
                          " (" +
                          "   select Id, Name, ParentId, 0 as Level" +
                          "   from Equipment e" +
                          "   where e.Id = :id" +
                          "  union all" +
                          "   select e.Id, e.Name, e.ParentId, eh.Level + 1" +
                          "   from Equipment e" +
                          "   inner join Hierachy eh" +
                          "      on e.ParentId = eh.Id" +
                          " )" +
                          " select Id, Name, ParentId" +
                          " from Hierachy" +
                          " where Level > 0";
                var list = session.CreateSQLQuery(sql)
                    .AddEntity(typeof(Equipment))
                    .SetGuid("id", equipmentId)
                    .List<Equipment>();
                return list;
            }
        }

        public ICollection<Equipment> GetAllAncestorsOfEquipment(Guid equipmentId)
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "with Hierachy(Id, Name, ParentId, Level)" +
                          " as" +
                          " (" +
                          "   select Id, Name, ParentId, 0 as Level" +
                          "   from Equipment e" +
                          "   where e.Id = :id" +
                          "  union all" +
                          "   select e.Id, e.Name, e.ParentId, eh.Level + 1" +
                          "   from Equipment e" +
                          "   inner join Hierachy eh" +
                          "      on e.Id = eh.ParentId" +
                          " )" +
                          " select Id, Name, ParentId" +
                          " from Hierachy" +
                          " where Level > 0";
                var list = session.CreateSQLQuery(sql)
                    .AddEntity(typeof(Equipment))
                    .SetGuid("id", equipmentId)
                    .List<Equipment>();
                return list;
            }
        }
    }
}