using System.Collections.Generic;

namespace CollectionMapping
{
    public class DriveSystem
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<string> Equipments { get; set; }
        public virtual IList<Manual> Manuals { get; set; }

        public DriveSystem()
        {
            Equipments = new List<string>();
            Manuals = new List<Manual>();
        }
    }

    public class Manual
    {
        public virtual string Title { get; set; }
        public virtual string Language { get; set; }
    }
}