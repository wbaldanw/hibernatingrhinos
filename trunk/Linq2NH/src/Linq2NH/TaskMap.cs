using FluentNHibernate.Mapping;

namespace Linq2NH
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Id(x => x.Id);
            Map(x => x.TaskName);
            Map(x => x.DueDate);
        }
    }
}