using TennisCourt.Domain.Enums;

namespace TennisCourt.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        protected Guid Id { get; }
        protected DateTime CreatedAt { get; } = DateTime.Now;
        protected EntityStatusEnum Status { get; private set; } = EntityStatusEnum.Active;
        internal bool IsActive() => Status == EntityStatusEnum.Active;
        protected void Delete()
        {
            Status = EntityStatusEnum.Deleted;
        }

    }
}
