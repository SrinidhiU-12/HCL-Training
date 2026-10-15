
using System.ComponentModel.DataAnnotations;

namespace LeaveManagementWebApplication1.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(100)]
        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Leave type is required.")]
        [StringLength(50)]
        public string LeaveType { get; set; } = string.Empty;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Reason is required.")]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;

        [Required]
        public string Status { get; set; } = "Pending";
    }
}
