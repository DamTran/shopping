using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepository;
using Taste.Models;
using Taste.Utility;
using Taste.DataAccess.DbConnectionProvider;
using Taste.DataAccess;
using Dapper;

namespace Taste.Controllers
{
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
            EnumSqlQuery sqlQuery;
            if (!Enum.TryParse(queryName, true, out sqlQuery))
            {
                var exception = new Exception("Invalid API call.");
                return BadRequest(exception);
            }

            string query = _dbConnectionProvider.Queries[sqlQuery];
            using (var connection = _dbConnectionProvider.GetRemoteDbConnection())
            {
                var result = connection.Query(query);
                return Json(result);
            }
        }
    }
}