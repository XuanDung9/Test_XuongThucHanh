using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Test_XuongThucHanh.Controllers
{
    public class FacilityController : Controller
    {
        // GET: FacilityController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FacilityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FacilityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FacilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FacilityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FacilityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FacilityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FacilityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
