using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService)
        {
            _salesRecordService = salesRecordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? dtMin, DateTime? dtMax)
        {
            if (!dtMin.HasValue)
            {
                dtMin = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!dtMax.HasValue)
            {
                dtMax = DateTime.Now;
            }
            ViewData["minDate"] = dtMin.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = dtMax.Value.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(dtMin, dtMax);
            return View(result);
        }

        public IActionResult GroupingSearch()
        {
            return View();
        }
    }
}