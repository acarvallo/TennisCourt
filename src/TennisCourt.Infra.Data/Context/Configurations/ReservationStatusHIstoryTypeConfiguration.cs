using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisCourt.Domain.Models;
using TennisCourt.Infra.Data.Context.Configurations.Base;

namespace TennisCourt.Infra.Data.Context.Configurations
{
    public class ReservationStatusHIstoryTypeConfiguration : BaseEntityTypeConfiguration<ReservationStatusHistory>
    {
        public override void Configure(EntityTypeBuilder<ReservationStatusHistory> builder)
        {
            base.Configure(builder);
            builder
                 .Property(x => x.ReservationStatus)
                 .HasConversion<string>()
                 .HasMaxLength(20)
                 .IsRequired();

        }
    }
}
