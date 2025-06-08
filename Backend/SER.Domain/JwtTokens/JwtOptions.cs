namespace SER.Domain.JwtTokens;
public static class JwtOptions
{
	public static String Key => Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new Exception("JWT_KEY does not set");
	public static Int32 ExpiryHours => Int32.Parse(Environment.GetEnvironmentVariable("JWT_EXPIRE_HOURSE") ?? throw new Exception("JWT_EXPIRE_HOURSE does not set"));
}
