using System;

namespace Blobs.UniDirectional
{
    public class PersonPhoto
    {
        public virtual Guid Id { get; set; }
        public virtual byte[] Image { get; set; }
    }
}