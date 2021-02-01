namespace Taste.DataAccess.DbConnectionProvider
{
    using Dapper;
    using MySql.Data.MySqlClient;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SQLite;
    using System.Linq;

    public class DbConnectionProvider : IDbConnectionProvider
    {
        private const string GetAllLogicQuery = "SELECT * FROM SqlQuery";
        private readonly string _localConnectionString;
        private readonly string _remoteConnectionString;

        public DbConnectionProvider(string localConnectionString, string remoteConnectionString)
        {
            _localConnectionString = localConnectionString;
            _remoteConnectionString = remoteConnectionString;
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

        public SQLiteConnection GetLocalDbConnection()
        {
            return new SQLiteConnection(_localConnectionString);
        }

        public void RefreshQueries()
        {
            using (var connection = GetLocalDbConnection())
            {
                connection.Open();
                var sqlQueries = connection.Query<SqlQuery>(GetAllLogicQuery);
                _queries = sqlQueries.ToDictionary(q => q.Title, q => q.Content);
            }
        }

        public IDbConnection GetRemoteDbConnection()
        {
            return new MySqlConnection(_remoteConnectionString);
        }
    }
}