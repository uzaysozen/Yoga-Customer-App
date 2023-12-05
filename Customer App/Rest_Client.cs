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
        private readonly YogaClassDB db;
        private readonly App app;

        public List<ClassInstance> EventResponse { get; set; }
        public BookingResponse BookingResponse { get; set; }


        private string RestUrl = "https://stuiis.cms.gre.ac.uk/COMP1424CoreWS/comp1424cw/GetInstances";
        private string RestUrl_booking = "https://stuiis.cms.gre.ac.uk/COMP1424CoreWS/comp1424cw/SubmitBookings";


        public Rest_Client()
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            app = Application.Current as App;
            db = app.YogaClassDatabase;
        }


        public async Task<List<ClassInstance>> SendEventAsync()
        {
            Uri uri = new Uri(string.Format(RestUrl, string.Empty));

            try
            {
                StringContent content = new StringContent("userid=ekuv31", Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    EventResponse = JsonSerializer.Deserialize<List<ClassInstance>>(
                responseContent, _serializerOptions);

                    foreach (var classInstance in EventResponse)
                    {
                        db.SaveClassInstance(classInstance);
                    }
                }
            }
            catch (Exception ex)
            { Debug.WriteLine(@"\tERROR {0}", ex.Message); }

            Debug.WriteLine(EventResponse[0].teacher);

            return EventResponse;
        }

        public async Task<BookingResponse> SendBookingAsync()
        {
            Uri uri = new Uri(string.Format(RestUrl_booking, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize(app.User, _serializerOptions);
                Debug.WriteLine(json);
                StringContent content = new StringContent("jsonpayload=" + json, Encoding.UTF8, "application/x-www-form-urlencoded");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await
                        response.Content.ReadAsStringAsync();
                    BookingResponse =
                        JsonSerializer.Deserialize<BookingResponse>(
                        responseContent, _serializerOptions);
                }

            }
            catch (Exception e)
            {
                 Debug.WriteLine(@"\tERROR {0}", e.Message); 

            }
            return BookingResponse;
        }
    }
}
