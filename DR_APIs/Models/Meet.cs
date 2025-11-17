using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

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
        public string? MeetGUID { get; set; }



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

                    // City
                    meet.MeetGUID = NullIfEmpty(GetField("MeetGUID", fields));
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
            raw = raw.Trim().Trim('"' );

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

        /// <summary>
        /// Call the API endpoint /Meet/AddMeet to insert this meet.
        /// Returns the newly created MRef on success, or -1 on failure/empty response.
        /// </summary>
        public static async Task<int> AddMeetAsync(Meet meet, CancellationToken cancellationToken = default)
        {
            if (meet is null) throw new ArgumentNullException(nameof(meet));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            // Base URL can be overridden by setting environment variable API_BASE_URL, otherwise fallback to localhost.
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Meet/AddMeet";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var json = JsonSerializer.Serialize(meet, jsonOptions);

            using var client = new HttpClient(httpClientHandler);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson)) return -1;

            try
            {
                // Try deserialize as JSON number
                var id = JsonSerializer.Deserialize<int>(responseJson, jsonOptions);
                if (id != 0) 
                {
                    meet.MRef = id;
                    return id;
                }

                // If id == 0 it might still be valid, return it.
                if (id == 0) 
                {
                    meet.MRef = 0;
                    return 0;
                }
            }
            catch
            {
                // Fall through to try parse as plain text
            }

            // Fallback: try parse plain integer (handles responses like "123" or "\"123\"")
            if (int.TryParse(responseJson.Trim().Trim('"'), out var parsed))
            {
                meet.MRef = parsed;
                return parsed;
            }

            return -1;
        }

        /// <summary>
        /// Call the REST service /Meet/GetByGuid and return the Meet if found, or null if not found.
        /// Throws HttpRequestException for non-success (non-404) responses.
        /// </summary>
        public static async Task<Meet?> GetByGuidAsync(string meetGuid, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(meetGuid)) throw new ArgumentNullException(nameof(meetGuid));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Meet/GetByGuid?meetGuid={Uri.EscapeDataString(meetGuid)}";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var client = new HttpClient(httpClientHandler);
            using var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson)) return null;

            var meet = JsonSerializer.Deserialize<Meet>(responseJson, jsonOptions);
            return meet;
        }

        /// <summary>
        /// Call the REST service /Meet/DeleteByGuid to remove a meet (and related events/divesheets).
        /// Returns the number of meet rows deleted (as returned by the API), returns 0 if the meet was not found,
        /// or -1 on error/unparseable response.
        /// </summary>
        public static async Task<int> DeleteByGuidAsync(string meetGuid, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(meetGuid)) throw new ArgumentNullException(nameof(meetGuid));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Meet/DeleteByGuid?meetGuid={Uri.EscapeDataString(meetGuid)}";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var client = new HttpClient(httpClientHandler);
            using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);
            using var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return 0;

            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson)) return -1;

            try
            {
                var value = JsonSerializer.Deserialize<int>(responseJson, jsonOptions);
                if (value != 0 || responseJson.Trim().Trim('"') == "0")
                    return value;
            }
            catch
            {
                // try plain parse
            }

            if (int.TryParse(responseJson.Trim().Trim('"'), out var parsed))
            {
                return parsed;
            }

            return -1;
        }
    }
}
