using Test_XuongThucHanh.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;

namespace Test_XuongThucHanh.Controllers
{
    public class StaffController : Controller
    {
        long time = DateTime.Now.Ticks;
        private readonly exam_distribution_testContext _context;
        public StaffController(exam_distribution_testContext context)
        {
            _context = context;
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
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid ID");
            }

            var staff = _context.Staff
                .Where(s => s.Id == id)
                .Select(s => new
                {
                    s.StaffCode,
                    s.Name,
                    s.AccountFe,
                    s.AccountFpt,
                    DepartmentName = _context.DepartmentFacilities
                        .Where(df => df.IdStaff == s.Id)
                        .Select(df => _context.Departments
                            .Where(d => d.Id == df.IdDepartment)
                            .Select(d => d.Name)
                            .FirstOrDefault())
                        .FirstOrDefault(),
                    FacilityName = _context.DepartmentFacilities
                        .Where(df => df.IdStaff == s.Id)
                        .Select(df => _context.Facilities
                            .Where(f => f.Id == df.IdFacility)
                            .Select(f => f.Name)
                            .FirstOrDefault())
                        .FirstOrDefault(),
                    MajorName = _context.StaffMajorFacilities
                        .Where(smf => smf.IdStaff == s.Id)
                        .Select(smf => _context.MajorFacilities
                            .Where(mf => mf.Id == smf.IdMajorFacility)
                            .Select(mf => _context.Majors
                                .Where(m => m.Id == mf.IdMajor)
                                .Select(m => m.Name)
                                .FirstOrDefault())
                            .FirstOrDefault())
                        .FirstOrDefault()
                })
                .FirstOrDefault();

            if (staff == null)
            {
                return NotFound();
            }

            ViewBag.Staff = staff;

            return View();
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
                staff.Id = Guid.NewGuid();
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
                data.Name = staff.Name;
                data.AccountFpt = staff.AccountFpt;
                data.AccountFe = staff.AccountFe;
                data.StaffCode = staff.StaffCode;
                data.Status = staff.Status;
                data.LastModifiedDate = DateTimeOffset.Now.ToUnixTimeSeconds();

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
        public ActionResult UpdateStatus(Guid id, byte status)
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
        public IActionResult DownloadTemplate()
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var stream = new MemoryStream())
            {
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.Add("Data");

                    worksheet.Cells[1, 1].Value = "StaffCode";
                    worksheet.Cells[1, 2].Value = "Name";
                    worksheet.Cells[1, 3].Value = "AccountFe";
                    worksheet.Cells[1, 4].Value = "AccountFpt";
                    worksheet.Cells[1, 5].Value = "Bộ môn - Chuyên ngành ";

                    var allStaff = _context.Staff
                        .Select(s => new
                        {
                            s.StaffCode,
                            s.Name,
                            s.AccountFe,
                            s.AccountFpt,
                            DepartmentName = _context.DepartmentFacilities
                                .Where(df => df.IdStaff == s.Id)
                                .Select(df => _context.Departments
                                    .Where(d => d.Id == df.IdDepartment)
                                    .Select(d => d.Name)
                                    .FirstOrDefault())
                                .FirstOrDefault(),
                            FacilityName = _context.DepartmentFacilities
                                .Where(df => df.IdStaff == s.Id)
                                .Select(df => _context.Facilities
                                    .Where(f => f.Id == df.IdFacility)
                                    .Select(f => f.Name)
                                    .FirstOrDefault())
                                .FirstOrDefault(),
                            MajorName = _context.StaffMajorFacilities
                                .Where(smf => smf.IdStaff == s.Id)
                                .Select(smf => _context.MajorFacilities
                                    .Where(mf => mf.Id == smf.IdMajorFacility)
                                    .Select(mf => _context.Majors
                                        .Where(m => m.Id == mf.IdMajor)
                                        .Select(m => m.Name)
                                        .FirstOrDefault())
                                    .FirstOrDefault())
                                .FirstOrDefault()
                        })
                        .ToList();


                    int row = 2;
                    foreach (var staff in allStaff)
                    {
                        worksheet.Cells[row, 1].Value = staff.StaffCode;
                        worksheet.Cells[row, 2].Value = staff.Name;
                        worksheet.Cells[row, 3].Value = staff.AccountFe;
                        worksheet.Cells[row, 4].Value = staff.AccountFpt;
                        worksheet.Cells[row, 5].Value = $"{staff.DepartmentName} - {staff.MajorName} - {staff.FacilityName}";
                        row++;
                    }

                    package.Save();
                }

                stream.Position = 0;
                var fileName = "StaffData.xlsx";
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
        // GET: Staff/Import
        public IActionResult Import()
        {
            return View();
        }

        // POST: Staff/Import
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Không có file nào được chọn");
                return View();
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var staffCode = worksheet.Cells[row, 1].Text;
                        var name = worksheet.Cells[row, 2].Text;
                        var accountFe = worksheet.Cells[row, 3].Text;
                        var accountFpt = worksheet.Cells[row, 4].Text;
                        var departmentName = worksheet.Cells[row, 5].Text;

                        var staff = new Staff
                        {
                            Id = Guid.NewGuid(),
                            StaffCode = staffCode,
                            Name = name,
                            AccountFe = accountFe,
                            AccountFpt = accountFpt,
                            Status = 1, 
                            CreatedDate = DateTimeOffset.Now.ToUnixTimeSeconds()
                        };
                        _context.Staff.Add(staff);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }
    }
}

