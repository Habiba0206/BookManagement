using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Persistence.EntityConfigurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .HasIndex(b => b.Title)
            .IsUnique();

        builder
            .Property(b => b.Title)
            .IsRequired();

        builder.HasOne(b => b.User)
            .WithMany(u => u.Books)
            .HasForeignKey(b => b.UserId);
    }
}
