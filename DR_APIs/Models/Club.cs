using System.Text.Json;

namespace DR_APIs.Models
{
    public class Club
    {
        public string? Representing { get; set; }
        public string? TCode { get; set; }
        public bool Validated { get; set; }




        public static async Task<List<Club>> CheckClubsAsync(List<Club> clubs, System.Threading.CancellationToken cancellationToken = default)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            if (clubs is null) throw new ArgumentNullException(nameof(clubs));

            // Base URL can be overridden by setting environment variable API_BASE_URL, otherwise fallback to localhost.
            var baseUrl = System.Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Club/CheckClubs";

            var jsonOptions = new System.Text.Json.JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var json = System.Text.Json.JsonSerializer.Serialize(clubs, jsonOptions);

            using var client = new System.Net.Http.HttpClient(httpClientHandler);
            using var content = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");

            using var response = await client.PostAsync(requestUri, content, cancellationToken).ConfigureAwait(false);
            //response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson)) return new List<Club>();

            var result = System.Text.Json.JsonSerializer.Deserialize<List<Club>>(responseJson, jsonOptions);
            return result ?? new List<Club>();
        }

        /// <summary>
        /// Calls the API endpoint Club/FindClub to search clubs by a free-text string.
        /// Returns an empty list on empty/blank responses.
        /// </summary>
        public static async Task<List<Club>> FindClubAsync(string searchStr, CancellationToken cancellationToken = default)
        {
            if (searchStr is null) throw new ArgumentNullException(nameof(searchStr));

            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true;

            // Base URL can be overridden by setting environment variable API_BASE_URL, otherwise fallback to localhost.
            var baseUrl = Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://localhost:7034";
            var requestUri = $"{baseUrl.TrimEnd('/')}/Club/FindClub?searchStr={Uri.EscapeDataString(searchStr)}";

            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            using var client = new HttpClient(httpClientHandler);
            using var response = await client.GetAsync(requestUri, cancellationToken).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(responseJson)) return new List<Club>();

            var result = JsonSerializer.Deserialize<List<Club>>(responseJson, jsonOptions);
            return result ?? new List<Club>();
        }

    }
}
