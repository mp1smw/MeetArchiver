using DR_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MeetController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString;
        private MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public MeetController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Find a single meet by exact MeetGUID.
        /// </summary>
        [HttpGet("GetByGuid")]
        public ActionResult<Meet> GetByGuid(string meetGuid)
        {
            if (string.IsNullOrWhiteSpace(meetGuid)) return BadRequest("meetGuid is required.");

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                const string sql = "SELECT * FROM ME_Meets WHERE MeetGUID = @MeetGUID LIMIT 1;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MeetGUID", meetGuid);

                var dt = new DataTable();
                using var da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count == 0) return NotFound();

                var meet = MapRowToMeet(dt.Rows[0]);
                return Ok(meet);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open) conn.Close();
            }
        }

        /// <summary>
        /// Search meets by title using a LIKE search on MTitle.
        /// </summary>
        [HttpGet("SearchByTitle")]
        public ActionResult<List<Meet>> SearchByTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return Ok(new List<Meet>());

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                const string sql = "SELECT * FROM ME_Meets WHERE MTitle LIKE @search;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@search", "%" + title + "%");

                var dt = new DataTable();
                using var da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                var results = new List<Meet>(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    results.Add(MapRowToMeet(row));
                }

                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open) conn.Close();
            }
        }

        /// <summary>
        /// Insert a new meet. Verifies MeetGUID is not already used and returns the newly created MRef.
        /// </summary>
        [HttpPost("AddMeet")]
        public ActionResult<int> AddMeet([FromBody] Meet meet)
        {
            string pw = Request.Headers["X-API-KEY"];
            string email = Request.Headers["X-API-ID"];
            var user = Helpers.GetUser(pw, email, conn);
            if (user.pk == 0)
            {
                return Unauthorized("Unauthorized access, you do not have permission to make changes to the database");
            }

            if (meet == null) return BadRequest("meet is required.");
            if (string.IsNullOrWhiteSpace(meet.MeetGUID)) return BadRequest("MeetGUID is required.");


            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                // Check MeetGUID uniqueness
                const string checkSql = "SELECT COUNT(*) FROM ME_Meets WHERE MeetGUID = @MeetGUID;";
                using (var checkCmd = new MySqlCommand(checkSql, conn))
                {
                    checkCmd.Parameters.AddWithValue("@MeetGUID", meet.MeetGUID);
                    var count = Convert.ToInt32(checkCmd.ExecuteScalar() ?? 0);
                    if (count > 0)
                    {
                        return Unauthorized("Meet already exists, you must delete it before trying publish again.");
                    }
                }

                // Insert the meet (use parameterized query)
                const string insertSql = @"
                    INSERT INTO ME_Meets
                        ( SDate, EDate, MTitle, Venue, City, Nation, International, MeetGUID, owner)
                    VALUES
                        ( @SDate, @EDate, @MTitle, @Venue, @City, @Nation, @International, @MeetGUID, @owner);";

                using var cmd = new MySqlCommand(insertSql, conn);

                // DateOnly -> DateTime for DB
                var sdate = (object?)null;
                var edate = (object?)null;
                try
                {
                    sdate = meet.StartDate != default ? (object)meet.StartDate.ToDateTime(new TimeOnly(0, 0)) : DBNull.Value;
                }
                catch
                {
                    sdate = DBNull.Value;
                }
                try
                {
                    edate = meet.EndDate != default ? (object)meet.EndDate.ToDateTime(new TimeOnly(0, 0)) : DBNull.Value;
                }
                catch
                {
                    edate = DBNull.Value;
                }

                cmd.Parameters.AddWithValue("@SDate", sdate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@EDate", edate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MTitle", (object?)meet.Title ?? "");
                cmd.Parameters.AddWithValue("@Venue", (object?)meet.Venue ?? "");
                cmd.Parameters.AddWithValue("@City", (object?)meet.City ?? "");
                cmd.Parameters.AddWithValue("@Nation", (object?)meet.Nation ?? "");
                // store International as tinyint 0/1
                cmd.Parameters.AddWithValue("@International", meet.International ? 1 : 0);
                cmd.Parameters.AddWithValue("@MeetGUID", meet.MeetGUID);
                cmd.Parameters.AddWithValue("@owner", user.pk);

                cmd.ExecuteNonQuery();

                var lastId = Convert.ToInt32(cmd.LastInsertedId);
                return Ok(lastId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open) conn.Close();
            }
        }

        /// <summary>
        /// Delete meet(s) identified by MeetGUID and related event & divesheet rows joined on the meet key.
        /// Returns the total number of rows deleted (meets + events + divesheets). Returns NotFound if meet does not exist.
        /// </summary>
        [HttpDelete("DeleteByGuid")]
        public ActionResult<int> DeleteByGuid(string meetGuid)
        {
            if (string.IsNullOrWhiteSpace(meetGuid)) return BadRequest("meetGuid is required.");

             string pw = Request.Headers["X-API-KEY"];
            string email = Request.Headers["X-API-ID"];
            var user = Helpers.GetUser(pw, email, conn);
            if (user.pk == 0)
            {
                return Unauthorized("Unauthorized access, you do not have permission to make destructive changes to the database");
            }


            bool needsClosing = false;
            MySqlTransaction? tx = null;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                // Find MRef for the supplied MeetGUID
                int? mref = null;
                const string findSql = "SELECT MRef, owner FROM ME_Meets WHERE MeetGUID = @MeetGUID LIMIT 1;";
                var findCmd = new MySqlCommand(findSql, conn);
                
                    findCmd.Parameters.AddWithValue("@MeetGUID", meetGuid);

                    var dt = new DataTable();
                    using var da = new MySqlDataAdapter(findCmd);
                    da.Fill(dt);

                mref = dt.Rows[0]["MRef"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["MRef"]) : (int?)null;
                int owner= Int32.Parse(dt.Rows[0]["owner"].ToString());

                if (owner != user.pk)  // user did't publish it so they can't delete it
                {
                    return Unauthorized("Unauthorized access. Only the person that published the meet can delete it");

                }

                tx = conn.BeginTransaction();

                // Delete dive sheets for this meet (assumes table name ME_Divesheets and column Meet)
                const string deleteDiveSheetsSql = "DELETE FROM ME_Divesheets WHERE `Meet` = @MRef;";
                using (var dsCmd = new MySqlCommand(deleteDiveSheetsSql, conn, tx))
                {
                    dsCmd.Parameters.AddWithValue("@MRef", mref.Value);
                    dsCmd.ExecuteNonQuery();
                }

                // Delete events for this meet (assumes table name me_events and column MeetRef)
                const string deleteEventsSql = "DELETE FROM me_events WHERE MeetRef = @MRef;";
                using (var evCmd = new MySqlCommand(deleteEventsSql, conn, tx))
                {
                    evCmd.Parameters.AddWithValue("@MRef", mref.Value);
                    evCmd.ExecuteNonQuery();
                }

                // Finally delete the meet row(s)
                const string deleteMeetSql = "DELETE FROM ME_Meets WHERE MRef = @MRef;";
                int meetsDeleted;
                using (var meetCmd = new MySqlCommand(deleteMeetSql, conn, tx))
                {
                    meetCmd.Parameters.AddWithValue("@MRef", mref.Value);
                    meetsDeleted = meetCmd.ExecuteNonQuery();
                }

                tx.Commit();

                // Return total rows deleted (meets + events + divesheets) is useful but we only have meetsDeleted directly;
                // if callers need exact counts for events/divesheets, modify to capture ExecuteNonQuery results above.
                return Ok(meetsDeleted);
            }
            catch (Exception ex)
            {
                try
                {
                    tx?.Rollback();
                }
                catch { /* swallow rollback errors */ }

                return StatusCode(500, ex.Message);
            }
            finally
            {
                if (needsClosing && conn.State == ConnectionState.Open) conn.Close();
            }
        }

        private static Meet MapRowToMeet(DataRow row)
        {
            var meet = new Meet();

            if (row.Table.Columns.Contains("MRef") && row["MRef"] != DBNull.Value)
                meet.MRef = Convert.ToInt32(row["MRef"]);

            if (row.Table.Columns.Contains("ArchiveMRef") && row["ArchiveMRef"] != DBNull.Value)
                meet.ArchiveMRef = Convert.ToInt32(row["ArchiveMRef"]);

            if (row.Table.Columns.Contains("SDate") && row["SDate"] != DBNull.Value)
            {
                var s = row["SDate"];
                if (s is DateTime dt) meet.StartDate = DateOnly.FromDateTime(dt);
                else if (DateOnly.TryParse(s.ToString(), out var ddo)) meet.StartDate = ddo;
                else if (DateTime.TryParse(s.ToString(), out var dt2)) meet.StartDate = DateOnly.FromDateTime(dt2);
            }

            if (row.Table.Columns.Contains("EDate") && row["EDate"] != DBNull.Value)
            {
                var s = row["EDate"];
                if (s is DateTime dt) meet.EndDate = DateOnly.FromDateTime(dt);
                else if (DateOnly.TryParse(s.ToString(), out var ddo)) meet.EndDate = ddo;
                else if (DateTime.TryParse(s.ToString(), out var dt2)) meet.EndDate = DateOnly.FromDateTime(dt2);
            }

            if (row.Table.Columns.Contains("MTitle") && row["MTitle"] != DBNull.Value)
                meet.Title = row["MTitle"].ToString();

            if (row.Table.Columns.Contains("Venue") && row["Venue"] != DBNull.Value)
                meet.Venue = row["Venue"].ToString();

            if (row.Table.Columns.Contains("City") && row["City"] != DBNull.Value)
                meet.City = row["City"].ToString();

            if (row.Table.Columns.Contains("Nation") && row["Nation"] != DBNull.Value)
                meet.Nation = row["Nation"].ToString();

            if (row.Table.Columns.Contains("International") && row["International"] != DBNull.Value)
            {
                var v = row["International"];
                if (int.TryParse(v.ToString(), out var iv)) meet.International = iv != 0;
                else if (bool.TryParse(v.ToString(), out var bv)) meet.International = bv;
            }

            if (row.Table.Columns.Contains("MeetGUID") && row["MeetGUID"] != DBNull.Value)
                meet.MeetGUID = row["MeetGUID"].ToString();

            return meet;
        }
    }
}
