using ELISExtension.Interface;
using ELISExtension.Models.Requests;
using ELISExtension.Models.Responses;
using ELISExtension.Util;
using Microsoft.AspNetCore.Mvc;

namespace ELISExtension.Controllers
{
    [ApiController]
    [Route("api/lab")]
    public class LabController : ControllerBase
    {
        [HttpGet("order/list")]
        public async Task<IActionResult> OrderList()
        {
            var user = AuthUtil.GetCurrentUser(this.HttpContext);
            if (user == null)
                return Ok(new BaseResponse(403, "Forbidden."));

            var list = await ELISINT.GetOrderList(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);

            return Ok(new ListResponse(list));
        }

        [HttpPost("order/results")]
        public async Task<IActionResult> OrderResults([FromBody] ItemRequest req)
        {
            var user = AuthUtil.GetCurrentUser(this.HttpContext);
            if (user == null)
                return Ok(new BaseResponse(403, "Forbidden."));

            var results = await ELISINT.GetOrderResults(req.id);

            return Ok(new ItemResponse(results));
        }
    }
}
