using FluentNHibernate;

namespace RepositoryPattern
{
    public class MyModel : PersistenceModel
    {
        public MyModel()
        {
            addMappingsFromThisAssembly();
        }
    }
}