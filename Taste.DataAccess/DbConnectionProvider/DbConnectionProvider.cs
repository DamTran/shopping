namespace Taste.DataAccess.DbConnectionProvider
{
    using System.Data.SQLite;
    using System.Collections.Generic;
    using System.Linq;
    using Dapper;
    using MySql.Data.MySqlClient;
    using System.Data;

    public class DbConnectionProvider : IDbConnectionProvider
    {
        private const string LocalBaseQuery = "SELECT * FROM SqlQuery";
        private string _localConnectionString;
        private string _remoteConnectionString;

        public DbConnectionProvider(string localConnectionString, string remoteConnectionString)
        {
            _localConnectionString = localConnectionString;
            _remoteConnectionString = remoteConnectionString;
        }

        private IDictionary<EnumSqlQuery, string> _queries;
        public IDictionary<EnumSqlQuery, string> Queries
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
                var sqlQueries = connection.Query<SqlQuery>(LocalBaseQuery);
                _queries = sqlQueries.ToDictionary(q => q.SqlQueryId, q => q.Content);
            }
        }

        public IDbConnection GetRemoteDbConnection()
        {
            return new MySqlConnection(_remoteConnectionString);
        }
    }
}