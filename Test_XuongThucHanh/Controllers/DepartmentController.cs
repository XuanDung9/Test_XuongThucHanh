using Microsoft.AspNetCore.Mvc;

namespace Test_XuongThucHanh.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
