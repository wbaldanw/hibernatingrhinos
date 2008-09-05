using System;

namespace FluentMapping.Domain
{
    [Serializable]
    public class EntityBase : IEquatable<EntityBase>
    {
        public virtual long Id { get; set; }

        public virtual bool Equals(EntityBase obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return obj.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return Equals((EntityBase)obj);
        }

        public override int GetHashCode()
        {
            return (Id.GetHashCode() * 397) ^ GetType().GetHashCode();
        }

        public static bool operator ==(EntityBase left, EntityBase right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EntityBase left, EntityBase right)
        {
            return !Equals(left, right);
        }
    }
}