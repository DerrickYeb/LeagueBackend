using System.Data;

namespace Application.Abstractions.DataHelper;

public interface IDbConnector:ITranscientService
{
    IDbConnection CreateConnection(string ConnectionStr = "");
    string GetTeamSchema();
    string GetTeamResultSchema();
    string GetHistorySchema();
}