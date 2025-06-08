using SER.Database.Models.ConfigurationTools;

namespace SER.Database.Models.Users;
public class UserEntity : BaseEntity
{
	public String Login { get; set; } = default!;
	public String Password { get; set; } = default!;
}
