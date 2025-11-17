using DR_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString;
        private MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public EventController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

        [HttpPost("AddEvents")]
        public ActionResult<int> AddEvent([FromBody] List<Event> ev)
        {
            try
            {
                foreach (var e in ev)
                {
                    AddEvent(e);
                }
                return Ok(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }

            return -1;
        }

        /// <summary>
        /// Insert a new event record. Uses parameterized SQL. Returns newly created Event primary key (LastInsertedId) or -1 on error.
        /// </summary>
        [HttpPost("AddEvent")]
        public ActionResult<int> AddEvent([FromBody] Event ev)
        {
            if (ev == null) return BadRequest("Event is required.");

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                const string sql = @"
                    INSERT INTO me_events
                        (MeetRef, ERef, EDate, ETitle, ESex, Board,
                         S1Dives, S1DD, S1Groups, S2Dives, S2DD, S2Groups,
                         Judges, Novice,
                         Cat1, Cat2, Cat3, Cat4, Cat5, Cat6,
                         Synchro, Stage, Parts,
                         CopyMeet, CopyEvent,
                         MinAge, MaxAge, MaxDD,
                         EParent, MaxHeight,
                         RunningOrder, Placed, AutoDD,
                         Cat1Rounds, Cat2Rounds, Cat3Rounds, Cat4Rounds, Cat5Rounds, Cat6Rounds,
                         MinDD, DoNotAccumulate, S1DDMin, MinGroups, ExecJudges,
                         Target, ShortTitle, SecsPerDive, DoNotRank, TeamEvent
                        )
                    VALUES
                        (@MeetRef, @EventRef, @EDate, @ETitle, @ESex, @Board,
                         @S1Dives, @S1DD, @S1Groups, @S2Dives, @S2DD, @S2Groups,
                         @Judges, @Novice,
                         @Cat1, @Cat2, @Cat3, @Cat4, @Cat5, @Cat6,
                         @Synchro, @Stage, @Parts,
                         @CopyMeet, @CopyEvent,
                         @MinAge, @MaxAge, @MaxDD,
                         @EParent, @MaxHeight,
                         @RunningOrder, @Placed, @AutoDD,
                         @Cat1Rounds, @Cat2Rounds, @Cat3Rounds, @Cat4Rounds, @Cat5Rounds, @Cat6Rounds,
                         @MinDD, @DoNotAccumulate, @S1DDMin, @MinGroups, @ExecJudges,
                         @Target, @ShortTitle, @SecsPerDive, @DoNotRank, @TeamEvent
                        );";

                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@MeetRef", ev.MeetRef);
                // EventRef may be provided or left null - store as provided or DBNull
                cmd.Parameters.AddWithValue("@EventRef", ev.ERef != 0 ? (object)ev.ERef : DBNull.Value);
                cmd.Parameters.AddWithValue("@ArchiveERef", ev.ArchiveERef.HasValue ? (object)ev.ArchiveERef.Value : DBNull.Value);

                // EDate DateOnly -> DateTime
                if (ev.EDate != default)
                    cmd.Parameters.AddWithValue("@EDate", ev.EDate.ToDateTime(new TimeOnly(0, 0)));
                else
                    cmd.Parameters.AddWithValue("@EDate", DBNull.Value);

                cmd.Parameters.AddWithValue("@ETitle", (object?)ev.ETitle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ESex", (object?)ev.ESex ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Board", (object?)ev.Board ?? DBNull.Value);

                cmd.Parameters.AddWithValue("@S1Dives", ev.S1Dives.HasValue ? (object)ev.S1Dives.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@S1DD", ev.S1DD.HasValue ? (object)ev.S1DD.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@S1Groups", ev.S1Groups.HasValue ? (object)ev.S1Groups.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@S2Dives", ev.S2Dives.HasValue ? (object)ev.S2Dives.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@S2DD", ev.S2DD.HasValue ? (object)ev.S2DD.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@S2Groups", ev.S2Groups.HasValue ? (object)ev.S2Groups.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@Judges", ev.Judges.HasValue ? (object)ev.Judges.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Novice", ev.Novice.HasValue ? (ev.Novice.Value ? 1 : 0) : DBNull.Value);

                cmd.Parameters.AddWithValue("@Cat1", ev.Cat1.HasValue ? (ev.Cat1.Value ? 1 : 0) : "");
                cmd.Parameters.AddWithValue("@Cat2", ev.Cat2.HasValue ? (ev.Cat2.Value ? 1 : 0) : "");
                cmd.Parameters.AddWithValue("@Cat3", ev.Cat3.HasValue ? (ev.Cat3.Value ? 1 : 0) : "");
                cmd.Parameters.AddWithValue("@Cat4", ev.Cat4.HasValue ? (ev.Cat4.Value ? 1 : 0) : "");
                cmd.Parameters.AddWithValue("@Cat5", ev.Cat5.HasValue ? (ev.Cat5.Value ? 1 : 0) : "");
                cmd.Parameters.AddWithValue("@Cat6", ev.Cat6.HasValue ? (ev.Cat6.Value ? 1 : 0) : "");

                cmd.Parameters.AddWithValue("@Synchro", ev.Synchro.HasValue ? (ev.Synchro.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Stage", ev.Stage.HasValue ? (object)ev.Stage.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Parts", ev.Parts.HasValue ? (object)ev.Parts.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@CopyMeet", ev.CopyMeet.HasValue ? (ev.CopyMeet.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@CopyEvent", ev.CopyEvent.HasValue ? (ev.CopyEvent.Value ? 1 : 0) : DBNull.Value);

                cmd.Parameters.AddWithValue("@MinAge", ev.MinAge.HasValue ? (object)ev.MinAge.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@MaxAge", ev.MaxAge.HasValue ? (object)ev.MaxAge.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@MaxDD", ev.MaxDD.HasValue ? (object)ev.MaxDD.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@EParent", ev.EParent.HasValue ? (object)ev.EParent.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@MaxHeight", ev.MaxHeight.HasValue ? (object)ev.MaxHeight.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@RunningOrder", ev.RunningOrder.HasValue ? (object)ev.RunningOrder.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Placed", ev.Placed.HasValue ? (object)ev.Placed.Value : "");
                cmd.Parameters.AddWithValue("@AutoDD", ev.AutoDD.HasValue ? (ev.AutoDD.Value ? 1 : 0) : DBNull.Value);

                cmd.Parameters.AddWithValue("@Cat1Rounds", ev.Cat1Rounds.HasValue ? (object)ev.Cat1Rounds.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat2Rounds", ev.Cat2Rounds.HasValue ? (object)ev.Cat2Rounds.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat3Rounds", ev.Cat3Rounds.HasValue ? (object)ev.Cat3Rounds.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat4Rounds", ev.Cat4Rounds.HasValue ? (object)ev.Cat4Rounds.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat5Rounds", ev.Cat5Rounds.HasValue ? (object)ev.Cat5Rounds.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat6Rounds", ev.Cat6Rounds.HasValue ? (object)ev.Cat6Rounds.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@MinDD", ev.MinDD.HasValue ? (object)ev.MinDD.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DoNotAccumulate", ev.DoNotAccumulate.HasValue ? (ev.DoNotAccumulate.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@S1DDMin", ev.S1DDMin.HasValue ? (object)ev.S1DDMin.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@MinGroups", ev.MinGroups.HasValue ? (object)ev.MinGroups.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@ExecJudges", ev.ExecJudges.HasValue ? (object)ev.ExecJudges.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@Target", (object?)ev.Target ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ShortTitle", (object?)ev.ShortTitle ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SecsPerDive", ev.SecsPerDive.HasValue ? (object)ev.SecsPerDive.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DoNotRank", ev.DoNotRank.HasValue ? (ev.DoNotRank.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@TeamEvent", ev.TeamEvent.HasValue ? (ev.TeamEvent.Value ? 1 : 0) : DBNull.Value);

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
        /// Return all events for a given MeetRef (parameterized).
        /// </summary>
        [HttpGet("GetByMeetRef")]
        public ActionResult<List<Event>> GetByMeetRef(int meetRef)
        {
            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                const string sql = "SELECT * FROM me_events WHERE MeetRef = @MeetRef ORDER BY EventRef;";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MeetRef", meetRef);

                var dt = new DataTable();
                using var da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                var results = new List<Event>(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    results.Add(MapRowToEvent(row));
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

        private static Event MapRowToEvent(DataRow row)
        {
            var ev = new Event();

            if (row.Table.Columns.Contains("MeetRef") && row["MeetRef"] != DBNull.Value)
                ev.MeetRef = Convert.ToInt32(row["MeetRef"]);

            if (row.Table.Columns.Contains("EventRef") && row["EventRef"] != DBNull.Value)
                ev.ERef = Convert.ToInt32(row["EventRef"]);

            if (row.Table.Columns.Contains("ArchiveERef") && row["ArchiveERef"] != DBNull.Value)
                ev.ArchiveERef = Convert.ToInt32(row["ArchiveERef"]);

            if (row.Table.Columns.Contains("EDate") && row["EDate"] != DBNull.Value)
            {
                var s = row["EDate"];
                if (s is DateTime dt) ev.EDate = DateOnly.FromDateTime(dt);
                else if (DateOnly.TryParse(s.ToString(), out var ddo)) ev.EDate = ddo;
                else if (DateTime.TryParse(s.ToString(), out var dt2)) ev.EDate = DateOnly.FromDateTime(dt2);
            }

            if (row.Table.Columns.Contains("ETitle") && row["ETitle"] != DBNull.Value)
                ev.ETitle = row["ETitle"].ToString();

            if (row.Table.Columns.Contains("ESex") && row["ESex"] != DBNull.Value)
                ev.ESex = row["ESex"].ToString();

            if (row.Table.Columns.Contains("Board") && row["Board"] != DBNull.Value)
                ev.Board = row["Board"].ToString();

            ev.S1Dives = TryGetInt(row, "S1Dives");
            ev.S1DD = TryGetDecimal(row, "S1DD");
            ev.S1Groups = TryGetInt(row, "S1Groups");

            ev.S2Dives = TryGetInt(row, "S2Dives");
            ev.S2DD = TryGetDecimal(row, "S2DD");
            ev.S2Groups = TryGetInt(row, "S2Groups");

            ev.Judges = TryGetInt(row, "Judges");
            ev.Novice = TryGetBool(row, "Novice");

            ev.Cat1 = TryGetBool(row, "Cat1");
            ev.Cat2 = TryGetBool(row, "Cat2");
            ev.Cat3 = TryGetBool(row, "Cat3");
            ev.Cat4 = TryGetBool(row, "Cat4");
            ev.Cat5 = TryGetBool(row, "Cat5");
            ev.Cat6 = TryGetBool(row, "Cat6");

            ev.Synchro = TryGetBool(row, "Synchro");
            ev.Stage = TryGetInt(row, "Stage");
            ev.Parts = TryGetInt(row, "Parts");

            ev.CopyMeet = TryGetBool(row, "CopyMeet");
            ev.CopyEvent = TryGetBool(row, "CopyEvent");

            ev.MinAge = TryGetInt(row, "MinAge");
            ev.MaxAge = TryGetInt(row, "MaxAge");
            ev.MaxDD = TryGetDecimal(row, "MaxDD");

            ev.EParent = TryGetInt(row, "EParent");
            ev.MaxHeight = TryGetDecimal(row, "MaxHeight");

            ev.RunningOrder = TryGetInt(row, "RunningOrder");
            ev.Placed = TryGetBool(row, "Placed");
            ev.AutoDD = TryGetBool(row, "AutoDD");

            ev.Cat1Rounds = TryGetInt(row, "Cat1Rounds");
            ev.Cat2Rounds = TryGetInt(row, "Cat2Rounds");
            ev.Cat3Rounds = TryGetInt(row, "Cat3Rounds");
            ev.Cat4Rounds = TryGetInt(row, "Cat4Rounds");
            ev.Cat5Rounds = TryGetInt(row, "Cat5Rounds");
            ev.Cat6Rounds = TryGetInt(row, "Cat6Rounds");

            ev.MinDD = TryGetDecimal(row, "MinDD");
            ev.DoNotAccumulate = TryGetBool(row, "DoNotAccumulate");
            ev.S1DDMin = TryGetDecimal(row, "S1DDMin");
            ev.MinGroups = TryGetInt(row, "MinGroups");
            ev.ExecJudges = TryGetInt(row, "ExecJudges");

            if (row.Table.Columns.Contains("Target") && row["Target"] != DBNull.Value)
                ev.Target = row["Target"].ToString();

            if (row.Table.Columns.Contains("ShortTitle") && row["ShortTitle"] != DBNull.Value)
                ev.ShortTitle = row["ShortTitle"].ToString();

            ev.SecsPerDive = TryGetInt(row, "SecsPerDive");
            ev.DoNotRank = TryGetBool(row, "DoNotRank");
            ev.TeamEvent = TryGetBool(row, "TeamEvent");

            return ev;
        }

        private static int? TryGetInt(DataRow row, string col)
        {
            if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
            {
                if (int.TryParse(row[col].ToString(), out var v)) return v;
            }
            return null;
        }

        private static decimal? TryGetDecimal(DataRow row, string col)
        {
            if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
            {
                if (decimal.TryParse(row[col].ToString(), NumberStyles.Number, CultureInfo.InvariantCulture, out var v)) return v;
            }
            return null;
        }

        private static bool? TryGetBool(DataRow row, string col)
        {
            if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
            {
                var s = row[col].ToString();
                if (int.TryParse(s, out var iv)) return iv != 0;
                if (bool.TryParse(s, out var bv)) return bv;
            }
            return null;
        }
    }
}

