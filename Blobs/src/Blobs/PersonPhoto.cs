using System;

namespace Blobs
{
    public class PersonPhoto
    {
        public virtual Guid Id { get; set; }
        public virtual Person Owner { get; set; }
        public virtual byte[] Image { get; set; }
    }
}