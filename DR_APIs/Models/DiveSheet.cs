using Microsoft.VisualBasic.FileIO;
using System.Globalization;

namespace DR_APIs.Models
{
    public class DiveSheet
    {
        public int Meet { get; set; }
        public int Event { get; set; }
        public int DiverA { get; set; }
        public int? DiverB { get; set; }

        public int Round { get; set; }
        public int Attempt { get; set; }
        public int StartOrder { get; set; }

        public string? DiveNo { get; set; }
        public string? Position { get; set; }
        public int? Board { get; set; }

        public decimal? Tariff { get; set; }

        public decimal? J1 { get; set; }
        public decimal? J2 { get; set; }
        public decimal? J3 { get; set; }
        public decimal? J4 { get; set; }
        public decimal? J5 { get; set; }
        public decimal? J6 { get; set; }
        public decimal? J7 { get; set; }
        public decimal? J8 { get; set; }
        public decimal? J9 { get; set; }
        public decimal? J10 { get; set; }
        public decimal? J11 { get; set; }

        public decimal? JTot { get; set; }
        public decimal? Points { get; set; }
        public decimal? CumPoints { get; set; }

        public bool? Cat1 { get; set; }
        public bool? Cat2 { get; set; }
        public bool? Cat3 { get; set; }
        public bool? Cat4 { get; set; }
        public bool? Cat5 { get; set; }
        public bool? Cat6 { get; set; }

        public decimal? PrevTot { get; set; }
        public bool? Retired { get; set; }
        public decimal? Predict { get; set; }
        public bool? Guest { get; set; }

        public int? Place { get; set; }
        public int? EPRef { get; set; }
        public decimal? Penalty { get; set; }
        public int? PlaceOrder { get; set; }

        public decimal? PSFPlace { get; set; }

        public int? P1 { get; set; }
        public int? P2 { get; set; }
        public int? P3 { get; set; }
        public int? P4 { get; set; }
        public int? P5 { get; set; }
        public int? P6 { get; set; }

        /// <summary>
        /// Parse a CSV file (with header) into a list of <see cref="DiveSheet"/>.
        /// Header names expected (case-insensitive): "Meet","Event","DiverA","DiverB","Round","Attempt",
        /// "StartOrder","DiveNo","Position","Board","Tariff","J1".."J11","JTot","Points","CumPoints",
        /// "Cat1".."Cat6","PrevTot","Retired","Predict","Guest","Place","EPRef","Penalty","PlaceOrder",
        /// "PSFPlace","P1".."P6"
        /// Extra columns are ignored.
        /// </summary>
        public static List<DiveSheet> ParseDiveSheets(string filePath)
        {
            if (filePath is null) throw new ArgumentNullException(nameof(filePath));
            if (!File.Exists(filePath)) throw new FileNotFoundException("CSV file not found", filePath);

            var list = new List<DiveSheet>();

            using var parser = new TextFieldParser(filePath)
            {
                TextFieldType = FieldType.Delimited
            };
            parser.SetDelimiters(",");
            parser.HasFieldsEnclosedInQuotes = true;

            if (parser.EndOfData) return list;

            var headerFields = parser.ReadFields() ?? Array.Empty<string>();
            var index = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < headerFields.Length; i++)
            {
                var h = headerFields[i]?.Trim().Trim('"');
                if (!string.IsNullOrEmpty(h) && !index.ContainsKey(h)) index[h] = i;
            }

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields() ?? Array.Empty<string>();
                if (fields.Length == 0) continue;

                string GetField(string name)
                {
                    if (!index.TryGetValue(name, out var pos)) return string.Empty;
                    if (pos < 0 || pos >= fields.Length) return string.Empty;
                    return fields[pos] ?? string.Empty;
                }

                var d = new DiveSheet
                {
                    Meet = ParseInt(GetField("Meet")),
                    Event = ParseInt(GetField("Event")),
                    DiverA = ParseInt(GetField("DiverA")),
                    DiverB = ParseNullableInt(GetField("DiverB")),

                    Round = ParseInt(GetField("Round")),
                    Attempt = ParseInt(GetField("Attempt")),
                    StartOrder = ParseInt(GetField("StartOrder")),

                    DiveNo = NullIfEmpty(GetField("DiveNo")),
                    Position = NullIfEmpty(GetField("Position")),
                    Board = ParseNullableInt(GetField("Board")),

                    Tariff = ParseNullableDecimal(GetField("Tariff")),

                    J1 = ParseNullableDecimal(GetField("J1")),
                    J2 = ParseNullableDecimal(GetField("J2")),
                    J3 = ParseNullableDecimal(GetField("J3")),
                    J4 = ParseNullableDecimal(GetField("J4")),
                    J5 = ParseNullableDecimal(GetField("J5")),
                    J6 = ParseNullableDecimal(GetField("J6")),
                    J7 = ParseNullableDecimal(GetField("J7")),
                    J8 = ParseNullableDecimal(GetField("J8")),
                    J9 = ParseNullableDecimal(GetField("J9")),
                    J10 = ParseNullableDecimal(GetField("J10")),
                    J11 = ParseNullableDecimal(GetField("J11")),

                    JTot = ParseNullableDecimal(GetField("JTot")),
                    Points = ParseNullableDecimal(GetField("Points")),
                    CumPoints = ParseNullableDecimal(GetField("CumPoints")),

                    Cat1 = ParseNullableBool(GetField("Cat1")),
                    Cat2 = ParseNullableBool(GetField("Cat2")),
                    Cat3 = ParseNullableBool(GetField("Cat3")),
                    Cat4 = ParseNullableBool(GetField("Cat4")),
                    Cat5 = ParseNullableBool(GetField("Cat5")),
                    Cat6 = ParseNullableBool(GetField("Cat6")),

                    PrevTot = ParseNullableDecimal(GetField("PrevTot")),
                    Retired = ParseNullableBool(GetField("Retired")),
                    Predict = ParseNullableDecimal(GetField("Predict")),
                    Guest = ParseNullableBool(GetField("Guest")),

                    Place = ParseNullableInt(GetField("Place")),
                    EPRef = ParseNullableInt(GetField("EPRef")),
                    Penalty = ParseNullableDecimal(GetField("Penalty")),
                    PlaceOrder = ParseNullableInt(GetField("PlaceOrder")),

                    PSFPlace = ParseNullableDecimal(GetField("PSFPlace")),

                    P1 = ParseNullableInt(GetField("P1")),
                    P2 = ParseNullableInt(GetField("P2")),
                    P3 = ParseNullableInt(GetField("P3")),
                    P4 = ParseNullableInt(GetField("P4")),
                    P5 = ParseNullableInt(GetField("P5")),
                    P6 = ParseNullableInt(GetField("P6"))
                };

                list.Add(d);
            }

            return list;
        }

        private static string? NullIfEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s.Trim().Trim('"');

        private static int ParseInt(string raw)
        {
            raw = raw?.Trim().Trim('"') ?? string.Empty;
            if (int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var v)) return v;
            return 0;
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
            if (raw == "0") return false;
            if (raw == "1") return true;
            if (string.Equals(raw, "true", StringComparison.OrdinalIgnoreCase)) return true;
            if (string.Equals(raw, "false", StringComparison.OrdinalIgnoreCase)) return false;
            return null;
        }
    }
}
