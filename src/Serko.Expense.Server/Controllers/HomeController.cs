using Microsoft.AspNetCore.Mvc;

namespace Serko.Expense.Server.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}