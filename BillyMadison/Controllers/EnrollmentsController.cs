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
    [Route("api/Students/{studentId:int}/Enrollments")]
    public class EnrollmentsController : Controller
    {
        private readonly BillyMadisonContext _context;

        public EnrollmentsController(BillyMadisonContext context)
        {
            _context = context;
        }

        // GET: api/Students/42/Enrollments
        [HttpGet]
        public async Task<IActionResult> GetEnrollments([FromRoute] int studentId)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }

            var enrollments = await _context.Enrollments.Where(e => e.StudentId == studentId).ToListAsync();

            return Ok(enrollments);
        }

        // GET: api/Students/42/Enrollments/5
        [HttpGet]
        [Route("{enrollmentId:int}")]
        public async Task<IActionResult> GetEnrollment([FromRoute] int studentId, [FromRoute] int enrollmentId)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == enrollmentId);

            if (enrollment == null)
            {
                return NotFound();
            }

            return Ok(enrollment);
        }

        // PUT: api/Students/42/Enrollments/5
        [HttpPut]
        [Route("{enrollmentId:int}")]
        public async Task<IActionResult> PutEnrollment([FromRoute] int studentId, [FromRoute] int enrollmentId, [FromBody] Enrollment enrollment)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (enrollmentId != enrollment.Id)
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
                if (!EnrollmentExists(enrollmentId))
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

        // POST: api/Students/42/Enrollments
        [HttpPost]
        public async Task<IActionResult> PostEnrollment([FromRoute] int studentId, [FromBody] Enrollment enrollment)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }

            // TODO: Enrollment for this student and this course should not already exist (but, not currently passing in courseId)

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEnrollment", new { id = enrollment.Id }, enrollment);
        }

        // DELETE: api/Students/42/Enrollments/5
        [HttpDelete]
        [Route("{enrollmentId:int}")]
        public async Task<IActionResult> DeleteEnrollment([FromRoute] int studentId, [FromRoute] int enrollmentId)
        {
            if (!StudentExists(studentId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var enrollment = await _context.Enrollments.SingleOrDefaultAsync(m => m.Id == enrollmentId);
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

        private bool StudentExists(int id)
        {
            var student = _context.Students.SingleOrDefault(s => s.Id == id);
            return student != null;
        }
    }
}