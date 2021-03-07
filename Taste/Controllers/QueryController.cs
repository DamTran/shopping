namespace Taste.Controllers
{
    using Dapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Taste.DataAccess.DbConnectionProvider;
    using Taste.Utility.Enums;
    using Taste.Utility.Exceptions;

    [Route("api")]
    [ApiController]
    public class QueryController : Controller
    {
        private readonly ILogger<QueryController> _logger;
        private readonly ITasteDbConnectionProvider _dbConnectionProvider;

        public QueryController(ILogger<QueryController> logger, ITasteDbConnectionProvider dbConnectionProvider)
        {
            _logger = logger;
            _dbConnectionProvider = dbConnectionProvider;
        }

        [HttpGet("{queryName}")]
        public IActionResult Get(string queryName)
        {
            var selectedQuery = _dbConnectionProvider.Queries
                .FirstOrDefault(kvp => string.Equals(kvp.Key, queryName, StringComparison.OrdinalIgnoreCase));
            if (selectedQuery.Equals(default(KeyValuePair<string, string>)))
            {
                var exception = new HandledException(ErrorCodeEnum.InvalidApi);
                _logger.LogWarning(exception.ErrorNumber, exception.Message);
                return BadRequest(exception);
            }

            string query = selectedQuery.Value;
            using (var connection = _dbConnectionProvider.GetDbConnection())
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
                        , statusCode: (int)HttpStatusCode.InternalServerError
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
            using (var connection = _dbConnectionProvider.GetDbConnection())
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