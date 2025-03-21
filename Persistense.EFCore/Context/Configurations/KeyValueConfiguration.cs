using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistense.EFCore.DTOs.DTOs;

namespace Persistense.EFCore.DTOs.Configurations;

public class KeyValueConfiguration : IEntityTypeConfiguration<KeyValueRecord>
{
    public void Configure(EntityTypeBuilder<KeyValueRecord> builder)
    {
        builder.HasKey(ex => new { ex.Key, ex.OwnerId });

        builder
            .Property(ex => ex.OwnerId)
            .IsRequired();
        
        builder
            .Property(ex => ex.Key)
            .IsRequired()
            .HasColumnName("Key")
            .HasColumnType("varchar(255)");
        
        builder.Property(ex => ex.Value)
            .HasColumnType("varchar(255)")
            .HasColumnName("Value");
        
        builder.ToTable("Datas");

        builder.HasIndex(ex => ex.Key);
    }
}