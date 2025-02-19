using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Persistence.EntityConfigurations;

public class UserInfoVerificationCodeConfiguration : IEntityTypeConfiguration<UserInfoVerificationCode>
{
    public void Configure(EntityTypeBuilder<UserInfoVerificationCode> builder)
    {
        builder.HasOne<User>().WithMany().HasForeignKey(code => code.UserId);
    }
}