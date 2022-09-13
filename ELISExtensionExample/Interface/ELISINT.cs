using ELISExtension.Interface.Models;
using Newtonsoft.Json;
using System.Dynamic;

namespace ELISExtension.Interface
{
    public class ELISINT
    {
        public static string? Token = null;
        public static DateTime Expires;

        public static async Task<string?> GetToken()
        {
            if(Token == null || Expires < DateTime.UtcNow)
            {
                dynamic o = new ExpandoObject();
                o.apiKey = AppConfig.ELISAPIKEY;
                o.apiSecret = AppConfig.ELISAPISECRET;
                var content = new StringContent(JsonConvert.SerializeObject(o));
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                var client = new HttpClient();

                var resp = await client.PostAsync(AppConfig.ELISAPIURL + "/token", content);

                if(resp.IsSuccessStatusCode)
                {
                    dynamic? body = JsonConvert.DeserializeObject(await resp.Content.ReadAsStringAsync());
                    if(body != null && body.errorCode == 0)
                    {
                        Token = body.token;
                        return Token;
                    }
                }

            }
            return Token;
        }

        public static async Task<User?> Login(string username, string password)
        {
            string? token = await GetToken();

            dynamic oo = new ExpandoObject();
            oo.username = username;
            oo.password = password;
            oo.token = token;

            var content = new StringContent(JsonConvert.SerializeObject(oo));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var client = new HttpClient();

            var resp = await client.PostAsync(AppConfig.ELISAPIURL + "/auth/login", content);

            if (resp.IsSuccessStatusCode)
            {
                var o = JsonConvert.DeserializeObject<User>(await resp.Content.ReadAsStringAsync());
                if (o != null)
                {
                    if (o.errorCode == 0)
                    {
                        return o;
                    }
                }
            }

            return null;
        }

        public static async Task<List<Order>?> GetOrderList(DateTime fromDate, DateTime toDate)
        {
            string? token = await GetToken();

            dynamic oo = new ExpandoObject();
            oo.token = token;
            oo.fromDate = fromDate.ToString("yyyy-MM-dd HH:mm:ss");
            oo.toDate = toDate.ToString("yyyy-MM-dd HH:mm:ss");

            var content = new StringContent(JsonConvert.SerializeObject(oo));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var client = new HttpClient();

            var resp = await client.PostAsync(AppConfig.ELISAPIURL + "/lab/order/list", content);

            if (resp.IsSuccessStatusCode)
            {
                var o = JsonConvert.DeserializeObject<OrderList>(await resp.Content.ReadAsStringAsync());
                if (o != null)
                {
                    if (o.errorCode == 0)
                    {
                        return o.list;
                    }
                }
            }

            return null;
        }

        public static async Task<ResultList> GetOrderResults(int orderId)
        {
            string? token = await GetToken();

            dynamic oo = new ExpandoObject();
            oo.token = token;
            oo.id = orderId;

            var content = new StringContent(JsonConvert.SerializeObject(oo));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var client = new HttpClient();

            var resp = await client.PostAsync(AppConfig.ELISAPIURL + "/lab/order/results", content);

            if (resp.IsSuccessStatusCode)
            {
                var o = JsonConvert.DeserializeObject<ResultList>(await resp.Content.ReadAsStringAsync());
                return o;                
            }

            return null;
        }
    }
}


