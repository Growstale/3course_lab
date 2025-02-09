using ASPCMVC08.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASPCMVC08.Controllers
{
    [Authorize(Roles = "Employee, Master")]
    public class CalcController : Controller
    {
        [HttpGet]
        public IActionResult Index(CalcViewModel model)
        {
            return View("Calc", model);
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Sub(CalcViewModel model)
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
        public IActionResult Mul(CalcViewModel model)
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
        public IActionResult Div(CalcViewModel model)
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
        public IActionResult Sum(CalcViewModel model)
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




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        static bool ConvertToFloat(CalcViewModel model, out float temp1, out float temp2)
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
