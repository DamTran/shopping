namespace Taste.DataAccess.DbConnectionProvider
{
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;

    public class SqLiteTasteDbConnectionProvider : ITasteDbConnectionProvider
    {
        private const string GetAllLogicQuery = "SELECT * FROM SqlQuery";
        private readonly string _connectionString;

        public SqLiteTasteDbConnectionProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDictionary<string, string> _queries;

        public IDictionary<string, string> Queries
        {
            get
            {
                if (_queries == null)
                {
                    RefreshQueries();
                }
                return _queries;
            }
        }

        public IDbConnection GetDbConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public void RefreshQueries()
        {
            using (var connection = GetDbConnection())
            {
                connection.Open();
                var sqlQueries = connection.Query<SqlQuery>(GetAllLogicQuery);
                _queries = sqlQueries.ToDictionary(q => q.Title, q => q.Content);
            }
        }
    }
}