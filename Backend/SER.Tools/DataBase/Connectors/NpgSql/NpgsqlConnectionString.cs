using Microsoft.Extensions.Configuration;
using Npgsql;

namespace SER.Tools.DataBase.Connectors.NpgSql;

public class NpgsqlConnectionString : IConnectionString
{
    public static NpgsqlConnectionString Get(IConfiguration configuration, string key, NpgsqlConnectionString? defaultConnectionString = null, int? timeout = default)
    {
        string connection = configuration.GetConnectionString(key);

        if (!string.IsNullOrEmpty(connection))
        {
            try
            {
                return new NpgsqlConnectionString(connection, timeout);
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Connection '{key}' is wrong", nameof(key), ex);
            }
        }
        else if (defaultConnectionString != null)
        {
            return defaultConnectionString;
        }
        else
        {
            throw new ArgumentException($"Connection '{key}' is not found in appsettings.json", nameof(key));
        }
    }

    public string Value { get; }
    public NpgsqlConnectionStringBuilder Parsed { get; }

    public NpgsqlConnectionString(string value, int? timeout = default)
    {
        Parsed = new NpgsqlConnectionStringBuilder(value);

        if (timeout.HasValue)
        {
            Parsed.Timeout = timeout.Value;
        }

        Value = Parsed.ToString();
    }

    public override string ToString() => Parsed.Password switch
    {
        null => Value,
        "" => Value,
        _ => Value.Replace(Parsed.Password, "...", StringComparison.Ordinal),
    };
}