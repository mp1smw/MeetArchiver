using DR_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiversController : Controller
    {
        private readonly IConfiguration _config;
        private string ConnectionString;
        MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public DiversController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

        [HttpGet("GetDiver")]
        public IEnumerable<Diver> GetDiver(string FirstName, string LastName, int Born)
        {
            bool needsClosing = false;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
                needsClosing = true;
            }

            string sql = "SELECT * FROM me_divers WHERE @FirstName = FirstName " +
                "AND LastName = @LastName " +
                "AND Born = @Born";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Born", Born);

            DataTable dt = new DataTable();

            var _da = new MySqlDataAdapter(cmd);
            _da.Fill(dt);

            List<Diver> divers = new List<Diver>();
            foreach (DataRow row in dt.Rows)
            {
                Diver diver = new Diver();
                diver.ArchiveID = Convert.ToInt32(row["DRef"]);
                diver.FirstName = row["FirstName"].ToString();
                diver.LastName = row["LastName"].ToString();
                diver.Sex = row["Sex"].ToString();
                diver.Born = Convert.ToInt32(row["Born"]);
                diver.Representing = row["Representing"].ToString();
                diver.TCode = row["TCode"].ToString();
                diver.Validated = true;
                divers.Add(diver);
            }

            if(divers.Count == 1)
            {
                if (needsClosing)
                    conn.Close();
              
                return divers;
            }

            // didnlt find a unique match, try soundex
            sql = "SELECT * FROM me_divers WHERE soundex(@FirstName) = soundex(FirstName) " +
                "AND soundex(LastName) = soundex(@LastName) " +
                "AND Born>=(@Born-1) AND Born<=(@Born+1);";

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Born", Born);

            dt = new DataTable();

            _da = new MySqlDataAdapter(cmd);
            _da.Fill(dt);

            divers = new List<Diver>();
            foreach (DataRow row in dt.Rows)
            {
                Diver diver = new Diver();
                diver.ArchiveID = Convert.ToInt32(row["DRef"]);
                diver.FirstName = row["FirstName"].ToString();
                diver.LastName = row["LastName"].ToString();
                diver.Sex = row["Sex"].ToString();
                diver.Born = Convert.ToInt32(row["Born"]);
                diver.Representing = row["Representing"].ToString();
                diver.TCode = row["TCode"].ToString();
                divers.Add(diver);
            }

            if(needsClosing)
                conn.Close();
            return divers;
        }

        [HttpPost("CheckAthletes")] 
        public IEnumerable<Diver> CheckAthletes(List<Diver> divers)
        {
            conn.Open();
            for (int i=0; i<divers.Count();i++)
            {
                
                var matches = GetDiver(divers[i].FirstName, divers[i].LastName, divers[i].Born ?? 0).ToList();
                if (matches.Count() == 1 && matches[0].Validated)
                {
                    int id = divers[0].ID; // copy local ID
                    divers[i] = matches[0];
                    divers[i].ID = id;  // restore local ID
                }
                else
                {
                    divers[i].PossibleMatches = matches;
                    divers[i].Validated = false;
                }

            }

            conn.Close();
            return divers;
        }

    }
}
