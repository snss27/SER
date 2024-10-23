namespace SER.Tools.Types.Results;

public readonly struct AuthentificationResult
{
	public String? Token { get; }
	public String? Email { get; }
	public String? Login { get; }
	public String? FirstName { get; }
	public String? LastName { get; }

	public Error[] Errors { get; }
	public Boolean IsSuccess => !Errors?.Any() ?? true;

	public AuthentificationResult(String? token, String? email, String? login, String? firstName, String? lastName, IEnumerable<Error>? errors = null)
	{
		Token = token;
		Email = email;
		Login = login;
		FirstName = firstName;
		LastName = lastName;
		Errors = errors?.ToArray() ?? new Error[0];
	}

	public static AuthentificationResult Success(String? token, String? email = null, String? login = null, String? firstName = null, String? lastName = null)
	{
		return new(token, email, login, firstName, lastName);
	}

	public static AuthentificationResult Failed(IEnumerable<Error> errors)
	{
		return new(default, default, default, default, default, errors);
	}

	public static AuthentificationResult Failed(String error)
	{
		return new(default, default, default, default, default, new[] { new Error(error) });
	}
}
