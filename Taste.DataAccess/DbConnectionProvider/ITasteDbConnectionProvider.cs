namespace Taste.DataAccess.DbConnectionProvider
{
    using System.Collections.Generic;
    using System.Data;

    public interface ITasteDbConnectionProvider
    {
        IDictionary<string, string> Queries { get; }

        IDbConnection GetDbConnection();

        void RefreshQueries();
    }
}