namespace Taste.Controllers
{
    using Dapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using Taste.DataAccess.DbConnectionProvider;

    [Route("api")]
    [ApiController]
    public class QueryController : Controller
    {
        private readonly IDbConnectionProvider _dbConnectionProvider;

        public QueryController(IDbConnectionProvider dbConnectionProvider)
        {
            _dbConnectionProvider = dbConnectionProvider;
        }

        [HttpPost("{queryName}")]
        public IActionResult Get(string queryName, [FromBody] object data)
        {
            if (!_dbConnectionProvider.Queries.ContainsKey(queryName))
            {
                var exception = new Exception("Invalid API call.");
                return BadRequest(exception);
            }

            string query = _dbConnectionProvider.Queries[queryName];
            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                var result = connection.Query(query, data);
                return Json(result);
            }
        }
    }
}