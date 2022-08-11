using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using System.Security.Policy;
using NuGet.Common;

namespace FrontEnd.Helpers
{
    public class ServiceRepository
    {
        public HttpClient Client { get; set; }
        public ServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com/");
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
            System.Text.Encoding.ASCII.GetBytes(
               $"Abm4N3r7alpm9l1d5-Cm9ol_vL7yz34_E8kQpFiqpgahsMU7fcrT-0QK6b9FJjv_IbEuVaQbm9D_QU27:EK1q_F67oK2oCPZ6eaYLDmg81jqRQ4z_In7iLulhTYAO0WouJqBQegPLUPUXWvcwsFQWEObK0SwJZ-Gy")));



        }

        public ServiceRepository(string Token)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com/");
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Token);

        }
        public HttpResponseMessage PostAsyncEncoded(string url, Dictionary<string, string> dict)
        {
            
            return Client.PostAsync(url, new FormUrlEncodedContent(dict)).GetAwaiter().GetResult();
        }
        public HttpResponseMessage PostAsyncStringContent(string url, StringContent stringContent)
        {

            return Client.PostAsync(url, stringContent).GetAwaiter().GetResult();
        }
    }
}
