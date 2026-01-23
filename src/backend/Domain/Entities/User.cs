using Domain.Common;
using Domain.Constants;

namespace Domain.Entities;
public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string FullName { get; set; } =null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string ProfilePhotoUrl { get; set; } = FileData.Default;
    public string PasswordHash { get; set; } = null!;
    
    public int RoleId { get; set; }
    public Role Role { get; set; } = null!;
    public Cart Cart { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];
    public ICollection<Message> Messages { get; set; } = [];
}