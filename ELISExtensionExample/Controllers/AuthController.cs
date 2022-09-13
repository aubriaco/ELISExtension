using ELISExtension.Interface;
using ELISExtension.Interface.Models;
using ELISExtension.Models;
using ELISExtension.Models.Requests;
using ELISExtension.Models.Responses;
using ELISExtension.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text;

namespace ELISExtension.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("current")]
        public IActionResult Current()
        {
            var user = AuthUtil.GetCurrentUser(this.HttpContext);

            if (user == null)
                return Ok(new BaseResponse(403, "No user."));

            return Ok(new ItemResponse(user));
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return Ok(new BaseResponse());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest req)
        {

            var user = await ELISINT.Login(req.username, req.password);

            if (user != null)
            {
                HttpContext.Session.Set("user", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(user)));
                return Ok(new BaseResponse());
            }

            return Ok(new BaseResponse(400, "Access denied."));

        }
    }
}
