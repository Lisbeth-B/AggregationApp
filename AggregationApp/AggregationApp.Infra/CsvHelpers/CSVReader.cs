using AggregationApp.Core;
using CsvHelper;
using System.Globalization;
using System.Net.Http.Headers;

namespace AggregationApp.Infra.Http
{
    public class CSVReader : ICSVReader
    {
        private readonly HttpClient _httpClient;

        public CSVReader(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<T>> Get<T>(string url)
        {
            using (HttpRequestMessage? msg = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
                using (HttpResponseMessage? resp = await _httpClient.SendAsync(msg))
                {
                    resp.EnsureSuccessStatusCode();

                    using (Stream? s = await resp.Content.ReadAsStreamAsync())
                    using (StreamReader? sr = new StreamReader(s))
                    using (CsvReader? csvReader = new CsvReader(sr, CultureInfo.CurrentCulture))
                    {
                        return csvReader.GetRecords<T>().ToList();
                    }
                }
            }
        }
    }
}
