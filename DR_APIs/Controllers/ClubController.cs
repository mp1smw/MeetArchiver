using DR_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClubController : Controller
    {

        private readonly IConfiguration _config;
        private string ConnectionString;
        MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public ClubController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

        [HttpGet("FindClub")]
        public IEnumerable<Club> FindClub(string searchStr)
        {
            bool needsClosing = false;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                needsClosing = true;
            }

            string sql = "SELECT distinct Representing, TCode FROM ME_Divers WHERE Representing like @search OR  TCode like @search";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@search","%" + searchStr + "%");

            DataTable dt = new DataTable();

            var _da = new MySqlDataAdapter(cmd);
            _da.Fill(dt);

            List<Club> clubs = new List<Club>();
            foreach (DataRow row in dt.Rows)
            {
                Club c = new Club();
                c.Representing = row["Representing"].ToString();
                c.TCode = row["TCode"].ToString(); 
                c.Validated = true;
                clubs.Add(c);
            }

            if (needsClosing)
                conn.Close();
            return clubs;
        }

        [HttpGet("CheckClub")]
        public IEnumerable<Club> CheckClub(string representing, string tcode)
        {
            bool needsClosing = false;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                needsClosing = true;
            }

            string sql = "SELECT distinct Representing, TCode FROM ME_Divers WHERE Representing = @representing AND TCode = @tcode";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@representing", representing);
            cmd.Parameters.AddWithValue("@tcode", tcode);

            DataTable dt = new DataTable();

            var _da = new MySqlDataAdapter(cmd);
            _da.Fill(dt);

            List<Club> clubs = new List<Club>();
            foreach (DataRow row in dt.Rows)
            {
                Club c = new Club();
                c.Representing = row["Representing"].ToString();
                c.TCode = row["TCode"].ToString();
                c.Validated = true;
                clubs.Add(c);
            }

            if (needsClosing)
                conn.Close();
            return clubs;
        }

        [HttpPost("CheckClubs")]
        public IEnumerable<Club> CheckClubs(List<Club> clubs)
        {
            conn.Open();
            for (int i = 0; i < clubs.Count(); i++)
            {

                var matches = CheckClub(clubs[i].Representing, clubs[i].TCode).ToList();
                if(matches.Count() == 1)
                {
                    clubs[i].Validated = true;
                }
                else
                {
                    clubs[i].Validated = false;
                }

            }

            conn.Close();
            return clubs;
        }

    }
}
