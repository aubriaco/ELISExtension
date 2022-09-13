using ELISExtension.Interface.Models;
using ELISExtension.Models;
using Newtonsoft.Json;
using System.Text;

namespace ELISExtension.Util
{
    public class AuthUtil
    {
        public static User? GetCurrentUser(HttpContext context)
        {
            var data = context.Session.Get("user");
            if (data == null)
                return null;

            var o = JsonConvert.DeserializeObject<User>(Encoding.UTF8.GetString(data));
            return o;
        }
    }
}
