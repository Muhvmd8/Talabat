namespace Persistence.Data.Configurations;
internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(o => o.Price)
               .HasColumnType("decimal(8, 2)");

        builder.OwnsOne(o => o.Product);
    }
}
