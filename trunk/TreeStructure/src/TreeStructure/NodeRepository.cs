using System;

namespace TreeStructure
{
    public class NodeRepository
    {
        public ISessionManager SessionManager { get; set; }

        public NodeRepository()
        {
            SessionManager = TreeStructure.SessionManager.Instance;
        }

        public Node GetAggregateById(Guid nodeId)
        {
            using (var session = SessionManager.OpenSession())
            {
                var sql = "from Node e" +
                          " left join fetch e.Parent p" +
                          " left join fetch e.Children c" +
                          " where e.Id = :id";
                var node = session.CreateQuery(sql)
                    .SetGuid("id", nodeId)
                    .UniqueResult<Node>();

                // load the ancestors
                var sql2 = "from Node e" +
                          " left join fetch e.Ancestors a" +
                          " where e.Id = :id";
                node = session.CreateQuery(sql2)
                    .SetGuid("id", nodeId)
                    .UniqueResult<Node>();

                // load the descendants
                var sql3 = "from Node e" +
                          " left join fetch e.Descendants d" +
                          " where e.Id = :id";
                node = session.CreateQuery(sql3)
                    .SetGuid("id", nodeId)
                    .UniqueResult<Node>();

                return node;
            }
        }
    }
}