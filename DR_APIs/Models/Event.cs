using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace DR_APIs.Models
{
    public class Event
    {
        public int MeetRef { get; set; }
        public int ERef { get; set; }
        public int? ArchiveERef { get; set; }

        // Event date (CSV: yyyy-MM-dd)
        public DateOnly EDate { get; set; }

        public string? ETitle { get; set; }
        public string? ESex { get; set; }
        public string? Board { get; set; }

        // Series 1
        public int? S1Dives { get; set; }
        public decimal? S1DD { get; set; }
        public int? S1Groups { get; set; }

        // Series 2
        public int? S2Dives { get; set; }
        public decimal? S2DD { get; set; }
        public int? S2Groups { get; set; }

        public int? Judges { get; set; }

        // Flags (0/1 in CSV) - nullable to represent missing values
        public bool? Novice { get; set; }

        // Category flags (Cat1..Cat6)
        public bool? Cat1 { get; set; }
        public bool? Cat2 { get; set; }
        public bool? Cat3 { get; set; }
        public bool? Cat4 { get; set; }
        public bool? Cat5 { get; set; }
        public bool? Cat6 { get; set; }

        public bool? Synchro { get; set; }

        public int? Stage { get; set; }
        public int? Parts { get; set; }

        public bool? CopyMeet { get; set; }
        public bool? CopyEvent { get; set; }

        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }

        public decimal? MaxDD { get; set; }

        public int? EParent { get; set; }

        public decimal? MaxHeight { get; set; }

        public int? RunningOrder { get; set; }
        public bool? Placed { get; set; }

        public bool? AutoDD { get; set; }

        public int? Cat1Rounds { get; set; }
        public int? Cat2Rounds { get; set; }
        public int? Cat3Rounds { get; set; }
        public int? Cat4Rounds { get; set; }
        public int? Cat5Rounds { get; set; }
        public int? Cat6Rounds { get; set; }

        public decimal? MinDD { get; set; }
        public bool? DoNotAccumulate { get; set; }
        public decimal? S1DDMin { get; set; }
        public int? MinGroups { get; set; }
        public int? ExecJudges { get; set; }

        public string? Target { get; set; }
        public string? ShortTitle { get; set; }

        public int? SecsPerDive { get; set; }
        public bool? DoNotRank { get; set; }
        public bool? TeamEvent { get; set; }


        /// <summary>
        /// Parse a CSV file (with header) into a list of <see cref="Event"/>.
        /// Header names expected (case-insensitive): 
        /// "MeetRef","EventRef","EDate","ETitle","ESex","Board","S1Dives","S1DD","S1Groups",
        /// "S2Dives","S2DD","S2Groups","Judges","Novice","Cat1","Cat2","Cat3","Cat4","Cat5","Cat6",
        /// "Synchro","Stage","Parts","CopyMeet","CopyEvent","MinAge","MaxAge","MaxDD","EParent","MaxHeight",
        /// "RunningOrder","Placed","AutoDD","Cat1Rounds","Cat2Rounds","Cat3Rounds","Cat4Rounds","Cat5Rounds","Cat6Rounds",
        /// "MinDD","DoNotAccumulate","S1DDMin","MinGroups","ExecJudges","Target","ShortTitle","SecsPerDive","DoNotRank","TeamEvent"
        /// Extra columns are ignored.
        /// </summary>
        public static List<Event> ParseEvents(string filePath)
        {
            if (filePath is null) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("CSV file not found", filePath);

            var events = new List<Event>();

            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                if (parser.EndOfData) return events;

                // read header and build index
                var headerFields = parser.ReadFields() ?? Array.Empty<string>();
                var index = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headerFields.Length; i++)
                {
                    var h = headerFields[i]?.Trim().Trim('"');
                    if (!string.IsNullOrEmpty(h) && !index.ContainsKey(h))
                        index[h] = i;
                }

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields() ?? Array.Empty<string>();
                    if (fields.Length == 0) continue;

                    Event ev = new Event();

                    ev.MeetRef = ParseInt(GetField("MeetRef"), ev.MeetRef);
                    ev.ERef = ParseInt(GetField("EventRef"), ev.ERef);

                    if (TryParseDateOnly(GetField("EDate"), out var ed)) ev.EDate = ed;

                    ev.ETitle = NullIfEmpty(GetField("ETitle"));
                    ev.ESex = NullIfEmpty(GetField("ESex"));
                    ev.Board = NullIfEmpty(GetField("Board"));

                    ev.S1Dives = ParseNullableInt(GetField("S1Dives"));
                    ev.S1DD = ParseNullableDecimal(GetField("S1DD"));
                    ev.S1Groups = ParseNullableInt(GetField("S1Groups"));

                    ev.S2Dives = ParseNullableInt(GetField("S2Dives"));
                    ev.S2DD = ParseNullableDecimal(GetField("S2DD"));
                    ev.S2Groups = ParseNullableInt(GetField("S2Groups"));

                    ev.Judges = ParseNullableInt(GetField("Judges"));

                    ev.Novice = ParseNullableBool(GetField("Novice"));
                    ev.Cat1 = ParseNullableBool(GetField("Cat1"));
                    ev.Cat2 = ParseNullableBool(GetField("Cat2"));
                    ev.Cat3 = ParseNullableBool(GetField("Cat3"));
                    ev.Cat4 = ParseNullableBool(GetField("Cat4"));
                    ev.Cat5 = ParseNullableBool(GetField("Cat5"));
                    ev.Cat6 = ParseNullableBool(GetField("Cat6"));

                    ev.Synchro = ParseNullableBool(GetField("Synchro"));
                    ev.Stage = ParseNullableInt(GetField("Stage"));
                    ev.Parts = ParseNullableInt(GetField("Parts"));

                    ev.CopyMeet = ParseNullableBool(GetField("CopyMeet"));
                    ev.CopyEvent = ParseNullableBool(GetField("CopyEvent"));

                    ev.MinAge = ParseNullableInt(GetField("MinAge"));
                    ev.MaxAge = ParseNullableInt(GetField("MaxAge"));

                    ev.MaxDD = ParseNullableDecimal(GetField("MaxDD"));

                    ev.EParent = ParseNullableInt(GetField("EParent"));

                    ev.MaxHeight = ParseNullableDecimal(GetField("MaxHeight"));

                    ev.RunningOrder = ParseNullableInt(GetField("RunningOrder"));
                    ev.Placed = ParseNullableBool(GetField("Placed"));

                    ev.AutoDD = ParseNullableBool(GetField("AutoDD"));

                    ev.Cat1Rounds = ParseNullableInt(GetField("Cat1Rounds"));
                    ev.Cat2Rounds = ParseNullableInt(GetField("Cat2Rounds"));
                    ev.Cat3Rounds = ParseNullableInt(GetField("Cat3Rounds"));
                    ev.Cat4Rounds = ParseNullableInt(GetField("Cat4Rounds"));
                    ev.Cat5Rounds = ParseNullableInt(GetField("Cat5Rounds"));
                    ev.Cat6Rounds = ParseNullableInt(GetField("Cat6Rounds"));

                    ev.MinDD = ParseNullableDecimal(GetField("MinDD"));
                    ev.DoNotAccumulate = ParseNullableBool(GetField("DoNotAccumulate"));
                    ev.S1DDMin = ParseNullableDecimal(GetField("S1DDMin"));
                    ev.MinGroups = ParseNullableInt(GetField("MinGroups"));
                    ev.ExecJudges = ParseNullableInt(GetField("ExecJudges"));

                    ev.Target = NullIfEmpty(GetField("Target"));
                    ev.ShortTitle = NullIfEmpty(GetField("ShortTitle"));

                    ev.SecsPerDive = ParseNullableInt(GetField("SecsPerDive"));
                    ev.DoNotRank = ParseNullableBool(GetField("DoNotRank"));
                    ev.TeamEvent = ParseNullableBool(GetField("TeamEvent"));

                    events.Add(ev);

                    // local helper to safely get field by header name
                    string GetField(string name)
                    {
                        if (!index.TryGetValue(name, out var pos)) return string.Empty;
                        if (pos < 0 || pos >= fields.Length) return string.Empty;
                        return fields[pos] ?? string.Empty;
                    }
                }
            }

            return events;
        }

        private static string? NullIfEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim().Trim('"');

        private static int ParseInt(string raw, int defaultValue = 0)
        {
            if (int.TryParse(raw.Trim().Trim('"'), NumberStyles.Integer, CultureInfo.InvariantCulture, out var v)) return v;
            return defaultValue;
        }

        private static int? ParseNullableInt(string raw)
        {
            raw = raw?.Trim().Trim('"') ?? string.Empty;
            if (string.IsNullOrEmpty(raw)) return null;
            if (int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v)) return v;
            return null;
        }

        private static decimal? ParseNullableDecimal(string raw)
        {
            raw = raw?.Trim().Trim('"') ?? string.Empty;
            if (string.IsNullOrEmpty(raw)) return null;
            if (decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var v)) return v;
            return null;
        }

        private static bool? ParseNullableBool(string raw)
        {
            raw = raw?.Trim().Trim('"') ?? string.Empty;
            if (string.IsNullOrEmpty(raw)) return null;

            if (bool.TryParse(raw, out var b)) return b;

            // handle "0"/"1"
            if (raw == "0") return false;
            if (raw == "1") return true;

            // handle True/False variations
            if (string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase)) return true;
            if (string.Equals(raw, "false", StringComparison.OrdinalIgnoreCase)) return false;

            return null;
        }

        private static bool TryParseDateOnly(string? raw, out DateOnly result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(raw)) return false;
            raw = raw.Trim().Trim('"');

            if (DateOnly.TryParseExact(raw, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return true;

            if (DateOnly.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return true;

            if (DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
            {
                result = DateOnly.FromDateTime(dt);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Call the REST service /Event/AddEvent to insert an Event for this meet.
        /// Returns the newly created event id (ERef) on success, -1 on error.
        /// </summary>
        public static async Task<int> AddEventAsync(Event ev, CancellationToken cancellationToken = default)
        {
            if (ev is null) throw new ArgumentNullException(nameof(ev));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Event/AddEvent";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var json = JsonSerializer.Serialize(ev, jsonOptions);

            using var client = new HttpClient(httpClientHandler);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseJson)) return -1;

            try
            {
                var id = JsonSerializer.Deserialize<int>(responseJson, jsonOptions);
                if (id != 0)
                {
                    return id;
                }
            }
            catch
            {
                // ignore
            }

            return -1;
        }


        /// <summary>
        /// Call the REST service /Event/AddEvent to insert an Event for this meet.
        /// Returns the newly created event id (ERef) on success, -1 on error.
        /// </summary>
        public static async Task<int> AddEventsAsync(List<Event> ev, CancellationToken cancellationToken = default)
        {
            if (ev is null) throw new ArgumentNullException(nameof(ev));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Event/AddEvents";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var json = JsonSerializer.Serialize(ev, jsonOptions);

            using var client = new HttpClient(httpClientHandler);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (string.IsNullOrWhiteSpace(responseJson)) return -1;

            try
            {
                var id = JsonSerializer.Deserialize<int>(responseJson, jsonOptions);
                if (id != 0)
                {
                    return id;
                }
            }
            catch
            {
                // ignore
            }

            return -1;

        }
    }
}
