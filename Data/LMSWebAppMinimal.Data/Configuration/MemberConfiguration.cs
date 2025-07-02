using LMSWebAppMinimal.Domain.Base;
using LMSWebAppMinimal.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSWebAppMinimal.Data.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Configure to use separate table
            builder.ToTable("Members");
            
            // Configure one-to-many relationship with Books
            builder.HasMany(m => m.BorrowedBooks)
                .WithOne()
                .HasForeignKey("MemberId")
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}