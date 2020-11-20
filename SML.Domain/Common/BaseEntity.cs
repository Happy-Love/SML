using System;
using System.Collections.Generic;
using System.Text;

namespace SML.Domain.Common
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
