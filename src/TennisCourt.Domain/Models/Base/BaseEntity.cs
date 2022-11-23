using System.Collections.Generic;
using TennisCourt.Domain.Enums;

namespace TennisCourt.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public EntityStatusEnum Status { get; private set; } = EntityStatusEnum.Active;
        public bool IsActive() => Status == EntityStatusEnum.Active;
        public void Delete()
        {
            Status = EntityStatusEnum.Deleted;
        }

        public virtual bool IsValid(IList<string> errors)
        {
            errors = Array.Empty<string>();
            return true;
        }
    }
}
