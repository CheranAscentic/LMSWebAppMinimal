using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSWebAppMinimal.Data.Configuration
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            // Configure as derived type of BaseUser
            builder.HasBaseType<BaseUser>();
            
            // Any Staff-specific configurations can go here
        }
    }
}