using BookManagement.Domain.Common.Entities;
using BookManagement.Domain.Enums;

namespace BookManagement.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PasswordHash { get; set; }
    public int Age { get; set; }
    public bool IsEmailAddressVerified { get; set; }
    public Role Role { get; set; }
    public UserState UserState { get; set; }
    public UserSettings? UserSettings { get; set; }
}
