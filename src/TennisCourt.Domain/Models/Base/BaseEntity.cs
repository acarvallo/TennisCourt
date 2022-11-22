using TennisCourt.Domain.Enums;

namespace TennisCourt.Domain.Models.Base
{
    public abstract class BaseEntity
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; } = DateTime.Now;
        public EntityStatusEnum Status { get; private set; } = EntityStatusEnum.Active;
        internal bool IsActive() => Status == EntityStatusEnum.Active;
        protected void Delete()
        {
            Status = EntityStatusEnum.Deleted;
        }

    }
}
