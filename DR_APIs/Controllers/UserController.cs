using DR_APIs.Models;
using DR_APIs.Utils;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly IConfiguration _config;
        private string ConnectionString;
        MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public UserController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

 
        [HttpGet("GetUser")]
        public  User GetUser(string APIKey, string email)
        {
            return Helpers.GetUser(APIKey, email, conn);
        }

    }
}
