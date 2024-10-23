namespace SER.Tools.DataBase;

public static class NpsqlConfigurator
{
    public static void Configure()
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html?tabs=annotations
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
}
