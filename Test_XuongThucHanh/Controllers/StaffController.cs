using Test_XuongThucHanh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Test_XuongThucHanh.Controllers
{
    public class StaffController : Controller
    {
        long time = DateTime.Now.Ticks;
        private readonly exam_distribution_testContext _context;
        public StaffController(exam_distribution_testContext context)
        {
            _context=context;
        }
        // GET: StaffController
        public ActionResult Index()
        {
 
           var allStaff = _context.Staff.ToList();
            return View(allStaff);
        }
        // GET: MajorController/Details/5
        public IActionResult Details(Guid id)
        {
            var staff = _context.Staff.Find(id);


            return View(staff);
        }

        // GET: MajorController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StaffController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }
            if (_context.Staff.Any(s => s.StaffCode == staff.StaffCode))
            {
                ModelState.AddModelError("StaffCode", "Mã nhân viên đã tồn tại.");
            }
            if (_context.Staff.Any(s => s.AccountFe == staff.AccountFe))
            {
                ModelState.AddModelError("AccountFe", "Email FE đã tồn tại.");
            }
            if (_context.Staff.Any(s => s.AccountFpt == staff.AccountFpt))
            {
                ModelState.AddModelError("AccountFpt", "Email FPT đã tồn tại.");
            }
            try
            {
                staff.Id=Guid.NewGuid();
                staff.CreatedDate = DateTimeOffset.Now.ToUnixTimeSeconds(); 
                staff.Status = 1;
                _context.Staff.Add(staff);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Edit/5
        public ActionResult Edit(Guid id)
        {
            var data = _context.Staff.Find(id);
            return View(data);
        }

        // POST: StaffController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Staff staff)
        {
            if (_context.Staff.Any(s => s.StaffCode == staff.StaffCode && s.Id != id))
            {
                ModelState.AddModelError("StaffCode", "Mã nhân viên đã tồn tại.");
            }
            if (_context.Staff.Any(s => s.AccountFe == staff.AccountFe && s.Id != id))
            {
                ModelState.AddModelError("AccountFe", "Email FE đã tồn tại.");
            }
            if (_context.Staff.Any(s => s.AccountFpt == staff.AccountFpt && s.Id != id))
            {
                ModelState.AddModelError("AccountFpt", "Email FPT đã tồn tại.");
            }
            if (!ModelState.IsValid)
            {
                return View(staff);
            }
            try
            {
                var data = _context.Staff.Find(id);
                data.Name= staff.Name;
                data.AccountFpt = staff.AccountFpt;
                data.AccountFe= staff.AccountFe;
                data.StaffCode= staff.StaffCode;
                data.Status = staff.Status;
                data.LastModifiedDate= DateTimeOffset.Now.ToUnixTimeSeconds();

                _context.Staff.Update(data);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StaffController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StaffController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(Guid id, byte status )
        {
            try
            {
                var item = _context.Staff.Find(id);
                if (item == null)
                {
                    return NotFound();
                }
                item.Status = status;
                item.LastModifiedDate = DateTimeOffset.Now.ToUnixTimeSeconds();

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
