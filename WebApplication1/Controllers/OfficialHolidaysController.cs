using HR.ManagmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace HR.ManagmentSystem.Controllers
{
    public class OfficialHolidaysController(IpublicHolidays _IpublicHolidays) : Controller
    {
        private readonly IpublicHolidays _IpublicHolidays = _IpublicHolidays;

        public async Task<IActionResult> Index()
        {
            return View(await _IpublicHolidays.GetPublicHoliday());
        }

        public async Task<IActionResult> Create(PublicHoliday model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _IpublicHolidays.AddPublicHolidaysAsync(model);
            return RedirectToAction(nameof(Index));
        }

    }
}
