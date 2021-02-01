namespace Taste.DataAccess.DbConnectionProvider
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;

    public interface IDbConnectionProvider
    {
        IDictionary<string, string> Queries { get; }

        SQLiteConnection GetLocalDbConnection();

        void RefreshQueries();

        IDbConnection GetRemoteDbConnection();
    }
}