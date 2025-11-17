csharp DR_APIs\Controllers\DiveSheetController.cs
using DR_APIs.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Globalization;

namespace DR_APIs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiveSheetController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string ConnectionString;
        private MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();

        public DiveSheetController(IConfiguration configuration)
        {
            _config = configuration;
            ConnectionString = _config["MysqlConStr"];
            conn.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Insert a divesheet row. Returns LastInsertedId or -1 on error.
        /// </summary>
        [HttpPost("AddDiveSheet")]
        public ActionResult<int> AddDiveSheet([FromBody] DiveSheet ds)
        {
            if (ds == null) return BadRequest("divesheet is required.");

            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                const string sql = @"
INSERT INTO me_divesheets
    (`Meet`, `Event`, `DiverA`, `DiverB`, `Round`, `Attempt`, `StartOrder`,
     `DiveNo`, `Position`, `Board`, `Tariff`,
     `J1`,`J2`,`J3`,`J4`,`J5`,`J6`,`J7`,`J8`,`J9`,`J10`,`J11`,
     `JTot`,`Points`,`CumPoints`,
     `Cat1`,`Cat2`,`Cat3`,`Cat4`,`Cat5`,`Cat6`,
     `PrevTot`,`Retired`,`Predict`,`Guest`,
     `Place`,`EPRef`,`Penalty`,`PlaceOrder`,
     `PSFPlace`,`P1`,`P2`,`P3`,`P4`,`P5`,`P6`)
VALUES
    (@Meet, @Event, @DiverA, @DiverB, @Round, @Attempt, @StartOrder,
     @DiveNo, @Position, @Board, @Tariff,
     @J1,@J2,@J3,@J4,@J5,@J6,@J7,@J8,@J9,@J10,@J11,
     @JTot,@Points,@CumPoints,
     @Cat1,@Cat2,@Cat3,@Cat4,@Cat5,@Cat6,
     @PrevTot,@Retired,@Predict,@Guest,
     @Place,@EPRef,@Penalty,@PlaceOrder,
     @PSFPlace,@P1,@P2,@P3,@P4,@P5,@P6);";

                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Meet", ds.Meet);
                cmd.Parameters.AddWithValue("@Event", ds.Event);
                cmd.Parameters.AddWithValue("@DiverA", ds.DiverA);
                cmd.Parameters.AddWithValue("@DiverB", ds.DiverB.HasValue ? (object)ds.DiverB.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Round", ds.Round);
                cmd.Parameters.AddWithValue("@Attempt", ds.Attempt);
                cmd.Parameters.AddWithValue("@StartOrder", ds.StartOrder);

                cmd.Parameters.AddWithValue("@DiveNo", (object?)ds.DiveNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Position", (object?)ds.Position ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Board", ds.Board.HasValue ? (object)ds.Board.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Tariff", ds.Tariff.HasValue ? (object)ds.Tariff.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@J1", ds.J1.HasValue ? (object)ds.J1.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J2", ds.J2.HasValue ? (object)ds.J2.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J3", ds.J3.HasValue ? (object)ds.J3.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J4", ds.J4.HasValue ? (object)ds.J4.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J5", ds.J5.HasValue ? (object)ds.J5.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J6", ds.J6.HasValue ? (object)ds.J6.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J7", ds.J7.HasValue ? (object)ds.J7.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J8", ds.J8.HasValue ? (object)ds.J8.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J9", ds.J9.HasValue ? (object)ds.J9.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J10", ds.J10.HasValue ? (object)ds.J10.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@J11", ds.J11.HasValue ? (object)ds.J11.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@JTot", ds.JTot.HasValue ? (object)ds.JTot.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Points", ds.Points.HasValue ? (object)ds.Points.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@CumPoints", ds.CumPoints.HasValue ? (object)ds.CumPoints.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@Cat1", ds.Cat1.HasValue ? (ds.Cat1.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat2", ds.Cat2.HasValue ? (ds.Cat2.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat3", ds.Cat3.HasValue ? (ds.Cat3.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat4", ds.Cat4.HasValue ? (ds.Cat4.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat5", ds.Cat5.HasValue ? (ds.Cat5.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Cat6", ds.Cat6.HasValue ? (ds.Cat6.Value ? 1 : 0) : DBNull.Value);

                cmd.Parameters.AddWithValue("@PrevTot", ds.PrevTot.HasValue ? (object)ds.PrevTot.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Retired", ds.Retired.HasValue ? (ds.Retired.Value ? 1 : 0) : DBNull.Value);
                cmd.Parameters.AddWithValue("@Predict", ds.Predict.HasValue ? (object)ds.Predict.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Guest", ds.Guest.HasValue ? (ds.Guest.Value ? 1 : 0) : DBNull.Value);

                cmd.Parameters.AddWithValue("@Place", ds.Place.HasValue ? (object)ds.Place.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@EPRef", ds.EPRef.HasValue ? (object)ds.EPRef.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@Penalty", ds.Penalty.HasValue ? (object)ds.Penalty.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@PlaceOrder", ds.PlaceOrder.HasValue ? (object)ds.PlaceOrder.Value : DBNull.Value);

                cmd.Parameters.AddWithValue("@PSFPlace", ds.PSFPlace.HasValue ? (object)ds.PSFPlace.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P1", ds.P1.HasValue ? (object)ds.P1.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P2", ds.P2.HasValue ? (object)ds.P2.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P3", ds.P3.HasValue ? (object)ds.P3.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P4", ds.P4.HasValue ? (object)ds.P4.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P5", ds.P5.HasValue ? (object)ds.P5.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@P6", ds.P6.HasValue ? (object)ds.P6.Value : DBNull.Value);

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
        /// Return all divesheets for a given Event (Event column).
        /// </summary>
        [HttpGet("GetByEvent")]
        public ActionResult<List<DiveSheet>> GetByEvent(int eventRef)
        {
            return GetByQuery("SELECT * FROM me_divesheets WHERE `Event` = @Key ORDER BY `Round`, `StartOrder`;", "@Key", eventRef);
        }

        /// <summary>
        /// Return all divesheets for a given Meet.
        /// </summary>
        [HttpGet("GetByMeet")]
        public ActionResult<List<DiveSheet>> GetByMeet(int meet)
        {
            return GetByQuery("SELECT * FROM me_divesheets WHERE `Meet` = @Key ORDER BY `Event`, `Round`, `StartOrder`;", "@Key", meet);
        }

        /// <summary>
        /// Return all divesheets for a given DiverA.
        /// </summary>
        [HttpGet("GetByDiverA")]
        public ActionResult<List<DiveSheet>> GetByDiverA(int diverA)
        {
            return GetByQuery("SELECT * FROM me_divesheets WHERE DiverA = @Key ORDER BY `Meet`, `Event`, `Round`, `StartOrder`;", "@Key", diverA);
        }

        /// <summary>
        /// Return all divesheets for a given DiverB.
        /// </summary>
        [HttpGet("GetByDiverB")]
        public ActionResult<List<DiveSheet>> GetByDiverB(int diverB)
        {
            return GetByQuery("SELECT * FROM me_divesheets WHERE DiverB = @Key ORDER BY `Meet`, `Event`, `Round`, `StartOrder`;", "@Key", diverB);
        }

        // common helper to execute simple parameterized queries that return DiveSheet rows
        private ActionResult<List<DiveSheet>> GetByQuery(string sql, string paramName, object paramValue)
        {
            bool needsClosing = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    needsClosing = true;
                }

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue(paramName, paramValue);

                var dt = new DataTable();
                using var da = new MySqlDataAdapter(cmd);
                da.Fill(dt);

                var results = new List<DiveSheet>(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    results.Add(MapRowToDiveSheet(row));
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

        private static DiveSheet MapRowToDiveSheet(DataRow row)
        {
            var d = new DiveSheet();

            if (row.Table.Columns.Contains("Meet") && row["Meet"] != DBNull.Value)
                d.Meet = Convert.ToInt32(row["Meet"]);

            if (row.Table.Columns.Contains("Event") && row["Event"] != DBNull.Value)
                d.Event = Convert.ToInt32(row["Event"]);

            if (row.Table.Columns.Contains("DiverA") && row["DiverA"] != DBNull.Value)
                d.DiverA = Convert.ToInt32(row["DiverA"]);

            if (row.Table.Columns.Contains("DiverB") && row["DiverB"] != DBNull.Value)
                d.DiverB = Convert.ToInt32(row["DiverB"]);

            if (row.Table.Columns.Contains("Round") && row["Round"] != DBNull.Value)
                d.Round = Convert.ToInt32(row["Round"]);

            if (row.Table.Columns.Contains("Attempt") && row["Attempt"] != DBNull.Value)
                d.Attempt = Convert.ToInt32(row["Attempt"]);

            if (row.Table.Columns.Contains("StartOrder") && row["StartOrder"] != DBNull.Value)
                d.StartOrder = Convert.ToInt32(row["StartOrder"]);

            if (row.Table.Columns.Contains("DiveNo") && row["DiveNo"] != DBNull.Value)
                d.DiveNo = row["DiveNo"].ToString();

            if (row.Table.Columns.Contains("Position") && row["Position"] != DBNull.Value)
                d.Position = row["Position"].ToString();

            d.Board = TryGetInt(row, "Board");

            d.Tariff = TryGetDecimal(row, "Tariff");

            d.J1 = TryGetDecimal(row, "J1");
            d.J2 = TryGetDecimal(row, "J2");
            d.J3 = TryGetDecimal(row, "J3");
            d.J4 = TryGetDecimal(row, "J4");
            d.J5 = TryGetDecimal(row, "J5");
            d.J6 = TryGetDecimal(row, "J6");
            d.J7 = TryGetDecimal(row, "J7");
            d.J8 = TryGetDecimal(row, "J8");
            d.J9 = TryGetDecimal(row, "J9");
            d.J10 = TryGetDecimal(row, "J10");
            d.J11 = TryGetDecimal(row, "J11");

            d.JTot = TryGetDecimal(row, "JTot");
            d.Points = TryGetDecimal(row, "Points");
            d.CumPoints = TryGetDecimal(row, "CumPoints");

            d.Cat1 = TryGetBool(row, "Cat1");
            d.Cat2 = TryGetBool(row, "Cat2");
            d.Cat3 = TryGetBool(row, "Cat3");
            d.Cat4 = TryGetBool(row, "Cat4");
            d.Cat5 = TryGetBool(row, "Cat5");
            d.Cat6 = TryGetBool(row, "Cat6");

            d.PrevTot = TryGetDecimal(row, "PrevTot");
            d.Retired = TryGetBool(row, "Retired");
            d.Predict = TryGetDecimal(row, "Predict");
            d.Guest = TryGetBool(row, "Guest");

            d.Place = TryGetInt(row, "Place");
            d.EPRef = TryGetInt(row, "EPRef");
            d.Penalty = TryGetDecimal(row, "Penalty");
            d.PlaceOrder = TryGetInt(row, "PlaceOrder");

            d.PSFPlace = TryGetDecimal(row, "PSFPlace");

            d.P1 = TryGetInt(row, "P1");
            d.P2 = TryGetInt(row, "P2");
            d.P3 = TryGetInt(row, "P3");
            d.P4 = TryGetInt(row, "P4");
            d.P5 = TryGetInt(row, "P5");
            d.P6 = TryGetInt(row, "P6");

            return d;
        }

        private static int? TryGetInt(DataRow row, string col)
        {
            if (row.Table.Columns.Contains(col) && row[col] != DBNull.Value)
            {
                if (int.TryParse(row[col].ToString(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var v)) return v;
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
                if (int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out var iv)) return iv != 0;
                if (bool.TryParse(s, out var bv)) return bv;
            }
            return null;
        }
    }
}