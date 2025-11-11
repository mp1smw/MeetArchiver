using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Text;

namespace DR_APIs.Models
{
    public class Meet
    {
        public int MRef { get; set; }
        public int? ArchiveMRef { get; set; }

        // Dates in CSV are yyyy-MM-dd -> use DateOnly for date-only values
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public string? Title { get; set; }
        public string? Venue { get; set; }
        public string? City { get; set; }

        // Nation contains ISO code (e.g. "GBR") or empty
        public string? Nation { get; set; }

        // CSV uses 0/1 for this column; expose as bool for clarity
        public bool International { get; set; }





        public static List<Meet> ParseMeets(string filePath)
        {
            var meets = new List<Meet>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                // Read header and map indices
                if (parser.EndOfData) return meets;
                string[] headers = parser.ReadFields()!;
                var idx = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < headers.Length; i++)
                {
                    var h = headers[i]?.Trim().Trim('"');
                    if (!string.IsNullOrEmpty(h)) idx[h] = i;
                }

                // Helper to get field by header name
                string GetField(string name, string?[] fields)
                {
                    if (!idx.TryGetValue(name, out var i)) return string.Empty;
                    if (i < 0 || i >= fields.Length) return string.Empty;
                    return fields[i] ?? string.Empty;
                }

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields()!;
                    if (fields.Length == 0) continue;

                    var meet = new Meet();

                    // MRef
                    var mrefRaw = GetField("MRef", fields);
                    if (int.TryParse(mrefRaw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var mref))
                        meet.MRef = mref;

                    // StartDate (SDate)
                    var sdateRaw = GetField("SDate", fields);
                    if (TryParseDateOnly(sdateRaw, out var sdate))
                        meet.StartDate = sdate;

                    // EndDate (EDate)
                    var edateRaw = GetField("EDate", fields);
                    if (TryParseDateOnly(edateRaw, out var edate))
                        meet.EndDate = edate;

                    // Title (MTitle)
                    meet.Title = NullIfEmpty(GetField("MTitle", fields));

                    // Venue
                    meet.Venue = NullIfEmpty(GetField("Venue", fields));

                    // City
                    meet.City = NullIfEmpty(GetField("City", fields));

                    // Note: CSV doesn't provide Nation/International in the sample; leave defaults

                    meets.Add(meet);
                }
            }

            return meets;
        }

        private static bool TryParseDateOnly(string? raw, out DateOnly result)
        {
            result = default;
            if (string.IsNullOrWhiteSpace(raw)) return false;
            raw = raw.Trim().Trim('"');

            // Try exact yyyy-MM-dd first, then fallback to DateTime parse
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

        private static string? NullIfEmpty(string? s) => string.IsNullOrWhiteSpace(s) ? null : s;
    }
}
