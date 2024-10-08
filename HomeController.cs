using Microsoft.AspNetCore.Mvc;
using WebLab1;

public class HomeController : Controller
{
    [HttpPost]
    public JsonResult Factorial(int number)
    {
        return Json(new { result = Calculations.Factorial(number) });
    }

    [HttpPost]
    public JsonResult Fibonacci(int number)
    {
        return Json(new { result = Calculations.Fibonacci(number) });
    }
}
