using Order = Domain.Models.OrderModule.Order;
namespace Persistence.Data.Configurations;
internal class OrderConfigurations : IEntityTypeConfiguration<Domain.Models.OrderModule.Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(o => o.SubTotal)
            .HasColumnType("decimal(8, 2)");

        builder.HasMany(o => o.Items)
            .WithOne();

        builder.HasOne(o => o.DeliveryMethod)
            .WithMany()
            .HasForeignKey(o => o.DeliveryMethodId);

        builder.OwnsOne(o => o.Address);
    }
}
