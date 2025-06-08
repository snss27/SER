using CSharpFunctionalExtensions;
using SER.Tools.Types.Results;

namespace SER.Domain.Services;
public interface IUsersService
{
	public Task<Result<String, Error>> Login(String login, String password);
}
