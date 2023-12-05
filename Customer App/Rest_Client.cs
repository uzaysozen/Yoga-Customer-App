using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Customer_App
{
    internal class Rest_Client
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;

        public string EventResponse { get; private set; }


        private string RestUrl = "https://stuiis.cms.gre.ac.uk/COMP1424CoreWS/comp1424cw/GetInstances";


        public Rest_Client()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }


        public async Task<ClassInstance> SendEventAsync()
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {

                StringContent content = new StringContent("userid=ekuv31", Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    EventResponse = await  response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(@"\tERROR {0}", ex.Message); }

            return EventResponse;

        }
    }
}
