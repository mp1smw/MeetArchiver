using DR_APIs.Models;
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
        public User GetUser(string APIKey)
        {
            bool needsClosing = false;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                needsClosing = true;
            }

            string sql = "SELECT  UserEmail, Role FROM meetmanagers WHERE APIKey = @apikey";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@apikey", APIKey);

            DataTable dt = new DataTable();

            var _da = new MySqlDataAdapter(cmd);
            _da.Fill(dt);

            User u = new User();
            foreach (DataRow row in dt.Rows)
            {
                u.UserEmail = row["UserEmail"].ToString();
                u.Role = row["Role"].ToString();
            }

            if (needsClosing)
                conn.Close();
            return u;
        }

    }
}
