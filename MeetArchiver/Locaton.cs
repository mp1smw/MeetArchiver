using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MeetArchiver
{
    public class Locaton
    {

        public string  GetCountryByIP()
        {
            var t = GetPublicIpAddress();
            t.Wait();
            string ip = t.Result;

            IpInfo ipInfo = new IpInfo();

            string info = new WebClient().DownloadString("http://ipinfo.io" + "/" + ip);

            ipInfo = System.Text.Json.JsonSerializer.Deserialize<IpInfo> (info);

            //JavaScriptSerializer jsonObject = new JavaScriptSerializer();
            //ipInfo = jsonObject.Deserialize<IpInfo>(info);

            RegionInfo region = new RegionInfo(ipInfo.country);

            Console.WriteLine(region.EnglishName);
            Console.ReadLine();
            return region.ThreeLetterISORegionName;

        }

        public class IpInfo
        {
            //country
            public string country { get; set; }
        }

        public async Task<string> GetPublicIpAddress()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Request API to get public IP
                    HttpResponseMessage response = await client.GetAsync("https://api.ipify.org?format=json");
                    response.EnsureSuccessStatusCode();

                    // Parse the returned JSON response
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject jsonObject = JObject.Parse(jsonResponse);

                    // Extract IP address
                    return jsonObject["ip"].ToString();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return null;
                }
            }
        }
    }

}
