using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Test_XuongThucHanh.Models;

namespace Test_XuongThucHanh.Controllers
{
    public class StaffMajorFacilytyController : Controller
    {
        private readonly exam_distribution_testContext _context;
        public StaffMajorFacilytyController(exam_distribution_testContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            var staffMajorFacilities  =  _context.StaffMajorFacilities.ToList();
            return View(staffMajorFacilities);
        }

        // GET: FacilityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FacilityController/Create
        public ActionResult Create()
        {
            var departments = _context.Departments.ToList();
            var majors = _context.Majors.ToList();
            var facilities = _context.Facilities.ToList();

            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            ViewBag.Majors = new SelectList(majors, "Id", "Name");
            ViewBag.Facilities = new SelectList(facilities, "Id", "Name");

            return View(new StaffMajorFacility());
        }

        // POST: FacilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StaffMajorFacility model)
        {
            if (ModelState.IsValid)
            {

                model.Id = Guid.NewGuid();
                model.CreatedDate = DateTimeOffset.Now.ToUnixTimeSeconds();
                model.LastModifiedDate = DateTimeOffset.Now.ToUnixTimeSeconds();

                // Lưu vào database
                _context.StaffMajorFacilities.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Majors = new SelectList(_context.Majors, "Id", "Name");
            ViewBag.Facilities = new SelectList(_context.Facilities, "Id", "Name");

            return View(model);
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
