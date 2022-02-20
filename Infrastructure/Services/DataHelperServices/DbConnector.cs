using System.Data;
using Application.Abstractions.DataHelper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Infrastructure.Services.DataHelperServices;

public class DbConnector:IDbConnector, IDisposable
{
    private readonly string DEFAULT_CONNECTION_STRING = "Database";
    private IDbConnection _connection;
    private readonly IConfiguration _configuration;

    public DbConnector(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IDbConnection CreateConnection(string ConnectionStr = "")
    {
        var connectionString = _configuration.GetConnectionString(string.IsNullOrWhiteSpace(ConnectionStr)
            ? DEFAULT_CONNECTION_STRING
            : ConnectionStr);
        _connection = new NpgsqlConnection(connectionString);
        return _connection;
    }

    public string GetTeamSchema()
    {
       return _configuration.GetConnectionString("TeamSchema");
    }

    public string GetTeamResultSchema()
    {
      return  _configuration.GetConnectionString("ClubResultSchema");
    }

    public string GetHistorySchema()
    {
       return _configuration.GetConnectionString("ClubHistorySchema");
    }

    public string GetStatisticsSchema()
    {
        return _configuration.GetConnectionString("ClubStatisticsSchema");
    }
    public void Dispose()
    {
      if(_connection.State is ConnectionState.Open)
         _connection?.Close();
         
      GC.SuppressFinalize(this);
    } 
}