namespace Taste.DataAccess.DbConnectionProvider
{
    using System.Data.SQLite;
    using System.Collections.Generic;
    using System.Data;

    public interface IDbConnectionProvider
    {
        IDictionary<EnumSqlQuery, string> Queries { get; }
        SQLiteConnection GetLocalDbConnection();
        void RefreshQueries();
        IDbConnection GetRemoteDbConnection();
    }
}