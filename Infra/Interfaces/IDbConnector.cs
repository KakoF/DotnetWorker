using System.Data;

namespace Infra.Interfaces
{
    public interface IDbConnector
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
    }
}
