using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_XuongThucHanh.Models
{
    public partial class Staff
    {
        public Staff()
        {
            DepartmentFacilities = new HashSet<DepartmentFacility>();
            StaffMajorFacilities = new HashSet<StaffMajorFacility>();
        }
        public byte? Status { get; set; }
        public long? CreatedDate { get; set; }
        public long? LastModifiedDate { get; set; }
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^\s@]+@fe\.edu\.vn$", ErrorMessage = "Email FE phải kế thúc bằng @fe.edu.vn")]
        public string? AccountFe { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^\s@]+@fpt\.edu\.vn$", ErrorMessage = "Email phải kết thúc bằng @fpt.edu.vn ")]
        public string? AccountFpt { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Tên không được quá 100 kí tự")]
        public string? Name { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Mã phải nhỏ hơn 15 kí tự")]
        public string? StaffCode { get; set; }

        public virtual ICollection<DepartmentFacility> DepartmentFacilities { get; set; }
        public virtual ICollection<StaffMajorFacility> StaffMajorFacilities { get; set; }
    }
}
