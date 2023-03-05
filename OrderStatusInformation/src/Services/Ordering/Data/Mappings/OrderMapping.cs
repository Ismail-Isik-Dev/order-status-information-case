using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Entities;

namespace Ordering.Data.Mappings
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.OrderNumber)
                .IsRequired();

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.Property(x => x.MaterialCode)
                .IsRequired();

            builder.Property(x => x.MaterialName)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(x => x.DestinationAddress)
               .HasMaxLength(500)
               .IsRequired();

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.QuantityUnit)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.WeightUnit)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Notes)
               .HasMaxLength(250);
        }
    }
}
