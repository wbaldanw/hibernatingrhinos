using System;

namespace Linq2NH
{
    public class Task
    {
        public virtual long Id { get; set; }
        public virtual string TaskName { get; set; }
        public virtual DateTime DueDate { get; set; }
    }
}