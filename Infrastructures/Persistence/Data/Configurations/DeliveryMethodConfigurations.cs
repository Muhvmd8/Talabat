
namespace Persistence.Data.Configurations;
internal class DeliveryMethodConfigurations 
    : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)");

        builder.Property(d => d.ShortName)
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder.Property(d => d.Description)
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder.Property(d => d.DeliveryTime)
            .HasColumnType("varchar")
            .HasMaxLength(100);
    }
}
