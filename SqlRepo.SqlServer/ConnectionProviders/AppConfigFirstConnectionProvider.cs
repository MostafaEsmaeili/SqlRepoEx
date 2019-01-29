using System.Configuration;

namespace SqlRepoEx.MsSqlServer.ConnectionProviders
{
    public class AppConfigFirstConnectionProvider : MsSqlConnectionProvider
    {
        public AppConfigFirstConnectionProvider()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings[0].ConnectionString;
        }
    }
}
