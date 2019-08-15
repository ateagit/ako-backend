using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ako_api.Models;
using ako_api.Models.DTO;
using AutoMapper;

namespace ako_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly AkoContext _context;

        private readonly IMapper _mapper;
        public CoursesController(AkoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OutputBasicCourseDTO>>> GetCourse()
        {
            List<Course> courses = await _context.Course
                .Include(c => c.Subject)
                .Include(c => c.User)
                .ToListAsync();

            List<OutputBasicCourseDTO> courseDTO = _mapper.Map<List<OutputBasicCourseDTO>>(courses);

            return courseDTO;
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutputDetailedCourseDTO>> GetCourse(int id)
        {
            Course course = await _context.Course
                .Include(c => c.Subject)
                .Include(c => c.User)
                .Include(c => c.CoursePrerequisiteMainCourse).ThenInclude(cq => cq.PrerequisiteCourse)
                .Include(c => c.Comment).ThenInclude(c => c.User)
                .SingleOrDefaultAsync(c => c.CourseId == id);

            OutputDetailedCourseDTO courseDTO = _mapper.Map<OutputDetailedCourseDTO>(course);

            if (course == null)
            {
                return NotFound();
            }

            return courseDTO;
        }

        // PUT: api/Courses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, InputCourseDTO courseDTO)
        {

            Course course = _mapper.Map<Course>(courseDTO);

            Course existingEntry = await _context.Course.FindAsync(id);

            if(existingEntry == null)
            {
                return BadRequest();
            }

            existingEntry.Content = course.Content;
            existingEntry.Title = course.Title;
            existingEntry.CoursePrerequisiteMainCourse = course.CoursePrerequisiteMainCourse;
            existingEntry.Rating = courseDTO.Difficulty;

            _context.Update(existingEntry);
            await _context.SaveChangesAsync();

            return Ok(existingEntry);
        }

        // POST: api/Courses
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(InputCourseDTO courseDTO)
        {
            // TODO: Validation
            Course course = _mapper.Map<Course>(courseDTO);


            _context.Course.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Course>> DeleteCourse(int id)
        {
            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return course;
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.CourseId == id);
        }
    }
}
