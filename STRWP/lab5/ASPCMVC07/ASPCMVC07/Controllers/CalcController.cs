using ASPCMVC07.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace ASPCMVC07.Controllers
{
    public class CalcController : Controller
    {
        private readonly ILogger<CalcController> _logger;

        public CalcController(ILogger<CalcController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index(CalcModel model)
        {
            return View("Calc", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Sub(CalcModel model)
        {
            model.press = "-";
            if (!string.IsNullOrEmpty(model.x) && !string.IsNullOrEmpty(model.y))
            {
                if (ConvertToFloat(model, out float num1, out float num2))
                {
                    model.result = num1 - num2;
                    model.error = null;
                }
                else
                {
                    model.error = "--ERROR--";
                    model.result = 0;
                }
            }
            return View("Calc", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Mul(CalcModel model)
        {
            model.press = "*";
            if (!string.IsNullOrEmpty(model.x) && !string.IsNullOrEmpty(model.y))
            {
                if (ConvertToFloat(model, out float num1, out float num2))
                {
                    model.result = num1 * num2;
                    model.error = null;
                }
                else
                {
                    model.error = "--ERROR--";
                    model.result = 0;
                }
            }
            return View("Calc", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Div(CalcModel model)
        {
            model.press = "/";

            if (!string.IsNullOrEmpty(model.x) && !string.IsNullOrEmpty(model.y))
            {
                if (ConvertToFloat(model, out float num1, out float num2))
                {
                    model.result = num1 / num2;
                    model.error = null;
                }
                else
                {
                    model.error = "--ERROR--";
                    model.result = 0;
                }
            }
            return View("Calc", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Sum(CalcModel model)
        {
            model.press = "+";
            if (!string.IsNullOrEmpty(model.x) && !string.IsNullOrEmpty(model.y))
            {
                if (ConvertToFloat(model, out float num1, out float num2))
                {
                    model.result = num1 + num2;
                    model.error = null;
                }
                else
                {
                    model.error = "--ERROR--";
                    model.result = 0;
                }
            }
            return View("Calc", model);
        }

        static bool ConvertToFloat(CalcModel model, out float temp1, out float temp2)
        {
            bool succeed = true;
            if (!float.TryParse(model.x, out temp1))
            {
                model.x = "0";
                succeed = false;
            }
            if (!float.TryParse(model.y, out temp2))
            {
                model.y = "0";
                succeed = false;
            }
            return succeed;
        }

    }
}
