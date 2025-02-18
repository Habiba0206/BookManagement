using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Persistance.DataContexts;
public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    public DbSet<UserInfoVerificationCode> UserInfoVerificationCodes => Set<UserInfoVerificationCode>();
    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookView> BookViews => Set<BookView>();
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    public DbSet<EmailHistory> EmailHistories => Set<EmailHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
