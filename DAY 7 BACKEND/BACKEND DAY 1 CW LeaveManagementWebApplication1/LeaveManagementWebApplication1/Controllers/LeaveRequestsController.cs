
using LeaveManagementWebApplication1.Data;
using LeaveManagementWebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementWebApplication1.Controllers
{
    [Route("api/leaverequests")]
    [ApiController]
    public class LeaveRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LeaveRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/leaverequests
        // GET: api/leaverequests?status=Pending
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaveRequest>>> GetAll(
            [FromQuery] string? status)
        {
            var query = _context.LeaveRequests.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                if (!IsValidStatus(status))
                {
                    return BadRequest(new
                    {
                        message = "Status must be Pending, Approved, or Rejected."
                    });
                }

                query = query.Where(x => x.Status == status);
            }

            var requests = await query.ToListAsync();

            return Ok(requests);
        }

        // GET: api/leaverequests/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeaveRequest>> GetById(int id)
        {
            var request = await _context.LeaveRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound(new
                {
                    message = "Leave request not found."
                });
            }

            return Ok(request);
        }

        // POST: api/leaverequests
        [HttpPost]
        public async Task<ActionResult<LeaveRequest>> Create(
            LeaveRequest request)
        {
            if (request.StartDate == default ||
                request.EndDate == default)
            {
                return BadRequest(new
                {
                    message = "Start date and end date are required."
                });
            }

            if (request.EndDate.Date < request.StartDate.Date)
            {
                return BadRequest(new
                {
                    message = "End date cannot be before start date."
                });
            }

            if (!string.IsNullOrWhiteSpace(request.Status) &&
                !IsValidStatus(request.Status))
            {
                return BadRequest(new
                {
                    message = "Status must be Pending, Approved, or Rejected."
                });
            }

            // Every new leave request starts as Pending.
            request.Id = 0;
            request.Status = "Pending";

            _context.LeaveRequests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = request.Id },
                request);
        }

        // PUT: api/leaverequests/1
        [HttpPut("{id:int}")]
        public async Task<ActionResult<LeaveRequest>> Update(
            int id,
            LeaveRequest updatedRequest)
        {
            var existing = await _context.LeaveRequests.FindAsync(id);

            if (existing == null)
            {
                return NotFound(new
                {
                    message = "Leave request not found."
                });
            }

            if (updatedRequest.StartDate == default ||
                updatedRequest.EndDate == default)
            {
                return BadRequest(new
                {
                    message = "Start date and end date are required."
                });
            }

            if (updatedRequest.EndDate.Date <
                updatedRequest.StartDate.Date)
            {
                return BadRequest(new
                {
                    message = "End date cannot be before start date."
                });
            }

            if (!IsValidStatus(updatedRequest.Status))
            {
                return BadRequest(new
                {
                    message = "Status must be Pending, Approved, or Rejected."
                });
            }

            existing.EmployeeName = updatedRequest.EmployeeName;
            existing.LeaveType = updatedRequest.LeaveType;
            existing.StartDate = updatedRequest.StartDate;
            existing.EndDate = updatedRequest.EndDate;
            existing.Reason = updatedRequest.Reason;
            existing.Status = NormalizeStatus(updatedRequest.Status);

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/leaverequests/1
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _context.LeaveRequests.FindAsync(id);

            if (request == null)
            {
                return NotFound(new
                {
                    message = "Leave request not found."
                });
            }

            _context.LeaveRequests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static bool IsValidStatus(string? status)
        {
            return status != null &&
                (status.Equals("Pending",
                    StringComparison.OrdinalIgnoreCase) ||
                 status.Equals("Approved",
                    StringComparison.OrdinalIgnoreCase) ||
                 status.Equals("Rejected",
                    StringComparison.OrdinalIgnoreCase));
        }

        private static string NormalizeStatus(string status)
        {
            if (status.Equals("Approved",
                StringComparison.OrdinalIgnoreCase))
                return "Approved";

            if (status.Equals("Rejected",
                StringComparison.OrdinalIgnoreCase))
                return "Rejected";

            return "Pending";
        }
    }
}
