using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamoDB.libs.DynamoDB;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GISProject.Controllers
{
    [Produces("application/json")]
    [Route("api/DynamoDB")]
    public class DynamoDBController : Controller
    {
        private readonly IDynamoDBExamples _dynamoDBExample;

        public DynamoDBController(IDynamoDBExamples DynamoDbExample)
        {
            _dynamoDBExample = DynamoDbExample;
        }

        [Route("CreateTable")] 
        // GET: /<controller>/
        public IActionResult CreateDynamoDbTable()
        {
            _dynamoDBExample.CreateDynamoDbTable();
            return Ok();
        }
    }
}
