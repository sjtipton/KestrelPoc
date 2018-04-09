using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillyMadison.Data;
using BillyMadison.Models;

namespace BillyMadison.Controllers
{
    [Produces("application/json")]
    [Route("api/Enrollments")]
    public class EnrollmentsController : Controller
    {
        private readonly BillyMadisonContext _context;

        public EnrollmentsController(BillyMadisonContext context)
        {
            _context = context;
        }

        // GET: api/Enrollments
        [HttpGet]
        public IEnumerable<Enrollment> GetEnrollments()
        {
            return _context.Enrollments;
        }

        // GET: api/Enrollments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEnrollment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == id);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // PUT: api/Enrollments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEnrollment([FromRoute] int id, [FromBody] Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollment.Id)
            {
                return BadRequest();
            }

            _context.Entry(enrollment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Enrollments
        [HttpPost]
        public async Task<IActionResult> PostEnrollment([FromBody] Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, enrollment);
        }

        // DELETE: api/Enrollments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnrollment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();

            return Ok(enrollment);
        }

        private bool EnrollmentExists(int id)
        {
            return _context.Enrollments.Any(e => e.Id == id);
        }
    }
}