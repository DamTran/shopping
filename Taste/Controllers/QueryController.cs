namespace Taste.Controllers
{
    using Dapper;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        [HttpGet("{queryName}")]
        public IActionResult Get(string queryName)
        {
            var selectedQuery = _dbConnectionProvider.Queries
                .FirstOrDefault(kvp => string.Equals(kvp.Key, queryName, StringComparison.OrdinalIgnoreCase));
            if (selectedQuery.Equals(default(KeyValuePair<string, string>)))
            {
                var exception = new Exception("Invalid API call.");
                return BadRequest(exception);
            }

            string query = selectedQuery.Value;
            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                try
                {
                    var result = connection.Query(query);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Problem(
                        detail: ex.StackTrace
                        , statusCode: 500
                        , title: ex.Message
                        , type: ex.GetType().FullName
                        );
                }
            }
        }

        [HttpPost("{queryName}")]
        public IActionResult Post(string queryName, [FromBody] object data)
        {
            var selectedQuery = _dbConnectionProvider.Queries
                .FirstOrDefault(kvp => string.Equals(kvp.Key, queryName, StringComparison.OrdinalIgnoreCase));
            if (selectedQuery.Equals(default(KeyValuePair<string, string>)))
            {
                var exception = new Exception("Invalid API call.");
                return BadRequest(exception);
            }

            string query = selectedQuery.Value;
            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                try
                {
                    var result = connection.Query(query, data);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Problem(
                        detail: ex.StackTrace
                        , statusCode: 500
                        , title: ex.Message
                        , type: ex.GetType().FullName
                        );
                }
            }
        }
    }
}