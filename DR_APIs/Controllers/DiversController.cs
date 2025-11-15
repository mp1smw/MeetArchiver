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
        public IEnumerable<Diver> GetDiver(string FirstName, string LastName, int Born, string Sex)
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
                diver.RecordStatus = RecordStatus.Valid;
                diver.PossibleMatches = new List<Diver>();
                divers.Add(diver);
            }

            if(divers.Count == 1)
            {
                if (needsClosing)
                    conn.Close();
              
                return divers;
            }

            // didnlt find a unique match, try soundex
            sql = "SELECT * FROM me_divers WHERE (soundex(@FirstName) = soundex(FirstName) " +
                "AND soundex(LastName) = soundex(@LastName) " +
                "AND Born>=(@Born-1) AND Born<=(@Born+1)) OR (LastName=@LastName AND Born=@Born AND Sex=@Sex);";

            cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@Born", Born);
            cmd.Parameters.AddWithValue("@Sex", Sex);

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
                diver.PossibleMatches = new List<Diver>();
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
                
                var matches = GetDiver(divers[i].FirstName, divers[i].LastName, divers[i].Born ?? 0, divers[i].Sex).ToList();
                if (matches.Count() == 1 && matches[0].RecordStatus== RecordStatus.Valid)
                {
                    int id = divers[i].ID; // copy local ID
                    divers[i] = matches[0];
                    divers[i].ID = id;  // restore local ID
                }
                else if(matches.Count() == 0)
                {
                    divers[i].RecordStatus = RecordStatus.New;
                }
                else
                {
                    divers[i].RecordStatus = RecordStatus.PossibleMatches;
                    divers[i].PossibleMatches = matches;
                }

            }

            conn.Close();
            return divers;
        }

        /// <summary>
        /// Update an existing diver record in the me_divers table using ArchiveID (DRef) as the WHERE key.
        /// Uses parameterized SQL and returns the number of rows affected (0 if no row updated), or -1 on error.
        /// </summary>
        [HttpPost("UpdateDiver")]
        public int UpdateDiver([FromBody] Diver diver)
        {
            if (diver == null || !diver.ArchiveID.HasValue) return -1;

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                var sql = @"UPDATE me_divers SET
                                FirstName = @FirstName,
                                LastName = @LastName,
                                Sex = @Sex,
                                Born = @Born,
                                Representing = @Representing,
                                TCode = @TCode,
                                Coach = @Coach,
                                Nation = @Nation
                            WHERE DRef = @DRef;";

                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@FirstName", (object?)diver.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", (object?)diver.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sex", (object?)diver.Sex ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Born", diver.Born.HasValue ? (object)diver.Born.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Representing", (object?)diver.Representing ?? "");
                cmd.Parameters.AddWithValue("@TCode", (object?)diver.TCode ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Coach", (object?)diver.Coach ?? "");
                cmd.Parameters.AddWithValue("@Nation", (object?)diver.Nation ?? "");
                cmd.Parameters.AddWithValue("@DRef", diver.ArchiveID.Value);

                int rows = cmd.ExecuteNonQuery();
                return rows;
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        /// <summary>
        /// Insert a new diver into the me_divers table using parameterized SQL.
        /// Returns the primary key (DRef) of the newly created record.
        /// </summary>
        [HttpPost("AddDiver")]
        public int AddDiver([FromBody] Diver diver)
        {
            if (diver == null) return -1;

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                // Insert using parameterized query. Columns chosen to match available Diver properties.
                var sql = @"INSERT INTO me_divers
                            (FirstName, LastName, Sex, Born, Representing, TCode, Coach, Nation, InsertDT)
                            VALUES
                            (@FirstName, @LastName, @Sex, @Born, @Representing, @TCode, @Coach, @Nation, @InsertDT);";

                using var cmd = new MySqlCommand(sql, conn);

                // Use DBNull.Value for nulls so DB can accept NULL where allowed.
                cmd.Parameters.AddWithValue("@FirstName", (object?)diver.FirstName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", (object?)diver.LastName ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sex", (object?)diver.Sex ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Born", diver.Born.HasValue ? (object)diver.Born.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Representing", (object?)diver.Representing ?? "");
                cmd.Parameters.AddWithValue("@TCode", (object?)diver.TCode ?? "");
                cmd.Parameters.AddWithValue("@Coach", (object?)diver.Coach ?? "");
                cmd.Parameters.AddWithValue("@Nation", (object?)diver.Nation ?? "");
                cmd.Parameters.AddWithValue("@InsertDT", DateTime.Now);


                cmd.ExecuteNonQuery();

                // MySqlCommand exposes LastInsertedId after ExecuteNonQuery
                var lastId = Convert.ToInt32(cmd.LastInsertedId);

                // Optionally set ArchiveID on returned object (not required)
                diver.ArchiveID = lastId;

                return lastId;
            }
            catch (Exception ex)
            {
                // log or handle as appropriate; returning 500 with message for simplicity
                return -1;
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        [HttpPost("ProcessDivers")]
        public IEnumerable<Diver> ProcessDivers(List<Diver> divers)
        {
            conn.Open();
            for (int i = 0; i < divers.Count(); i++)
            {
                if(divers[i].RecordStatus == RecordStatus.Valid)
                    continue;
                if (divers[i].RecordStatus == RecordStatus.New)
                {
                    var diverId = AddDiver(divers[i]);
                    divers[i].ArchiveID = diverId;
                    divers[i].RecordStatus = RecordStatus.Valid;
                }
                else if (divers[i].RecordStatus == RecordStatus.Updated)
                {
                    var rows = UpdateDiver(divers[i]);
                    divers[i].RecordStatus = RecordStatus.Valid;
                }

            }

            conn.Close();
            return divers;
        }

    }
}
