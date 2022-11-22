using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisCourt.Domain.Models;
using TennisCourt.Infra.Data.Context.Configurations.Base;

namespace TennisCourt.Infra.Data.Context.Configurations
{
    public class ReservationConfiguration : BaseEntityTypeConfiguration<Reservation>
    {

        public override void Configure(EntityTypeBuilder<Reservation> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(x => x.RefundAmount,
                            x => x.Property(p => p.Value)
                            .      HasColumnName("RefundAmount")
                                  .HasPrecision(18, 2) );

            builder.OwnsOne(x => x.Amount, r => r.Property(p => p.Value)
                                                    .IsRequired()
                                                    .HasColumnName("Amount")
                                                    .HasPrecision(18, 2));

            builder.HasMany(x => x.ReservationHistory)
                   .WithOne(x => x.Reservation)
                   .HasForeignKey(x => x.ReservationId);

            builder.Ignore(x => x.ReservationStatus);
        }
    }
}
