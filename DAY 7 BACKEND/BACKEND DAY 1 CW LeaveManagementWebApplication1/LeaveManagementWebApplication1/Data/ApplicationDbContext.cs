
using Microsoft.EntityFrameworkCore;
using LeaveManagementWebApplication1.Models;

namespace LeaveManagementWebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<LeaveRequest> LeaveRequests { get; set; }
    }
}
