using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class PrintController : Controller
    {
        public IActionResult PrintCountdownValue()
        {
            try
            {
                DateTime from = DateTime.Parse(Request.Form["FromDate"]);
                DateTime to = Convert.ToDateTime(Request.Form["ToDate"]);
                _Countdown countdown = new _Countdown(Actions.OrdersLoad(), from, to);
                return View("/Views/Home/Countdown.cshtml", countdown);
            }
            catch
            {
                return View("/Views/Home/Countdown.cshtml", new _Countdown());
            }
        }
    }
}
