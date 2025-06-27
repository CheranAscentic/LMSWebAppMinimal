using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Enum;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSWebAppMinimal.Data.Configuration
{
    public class BaseUserConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.HasKey(u => u.Id);
            
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
                
            // Configure discriminator for TPH inheritance
            builder.HasDiscriminator(u => u.Type)
                .HasValue<Member>(UserType.Member)
                .HasValue<Staff>(UserType.StaffMinor)
                .HasValue<Staff>(UserType.StaffManagement);
                
            // Make BaseUser abstract in the database schema
            builder.UseTphMappingStrategy();
        }
    }
}