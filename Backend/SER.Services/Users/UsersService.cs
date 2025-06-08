using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using SER.Database;
using SER.Database.Models.ConfigurationTools;
using SER.Database.Models.Users;
using SER.Domain.JwtTokens;
using SER.Domain.Services;
using SER.Tools.Types.Results;

namespace SER.Services.Users;
public class UsersService(SERDbContext dbContext, JwtProvider jwtProvider) : IUsersService
{
	public async Task<Result<String, Error>> Login(String login, String password)
	{
		UserEntity? user = await dbContext.Users
			.AsNoTracking()
			.FirstOrDefaultAsync(u => u.Login == login);

		if (user is null) return new Error("Пользователь не найден");

		if (user.Password != password) return new Error("Неправильный пароль");

		String jwtToken = jwtProvider.GenerateToken();

		return jwtToken;
	}
}
