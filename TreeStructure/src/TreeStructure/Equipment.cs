using System;
using Iesi.Collections.Generic;

namespace TreeStructure
{
    public class Equipment
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Equipment Parent { get; set; }
        public virtual ISet<Equipment> Children { get; set; }

        public Equipment()
        {
            Children = new HashedSet<Equipment>();
        }

        public virtual void AddChildEquipment(Equipment child)
        {
            Children.Add(child);
            child.Parent = this;
        }
    }
}
