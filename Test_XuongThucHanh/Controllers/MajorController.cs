using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test_XuongThucHanh.Models;

namespace Test_XuongThucHanh.Controllers
{
    public class MajorController : Controller
    {
        exam_distribution_testContext _context;
        // GET: MajorController
        public ActionResult Index()
        {
            var allStaff = _context.Staff.ToList();
            return View(allStaff);
        }

        // GET: MajorController/Details/5
        public ActionResult Details(Guid id)
        {
            var staff = _context.Staff.Find(id);
            return View(staff);
        }

        // GET: MajorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MajorController/Create
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

        // GET: MajorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MajorController/Edit/5
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

        // GET: MajorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MajorController/Delete/5
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
