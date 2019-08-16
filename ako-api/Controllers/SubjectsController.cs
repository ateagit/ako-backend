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
    public class SubjectsController : ControllerBase
    {
        private readonly AkoContext _context;
        private readonly IMapper _mapper;
        public SubjectsController(AkoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubject()
        {
            List<Subject> subjects = await _context.Subject
                .Include(s => s.SubjectHeirarchyParentSubject)
                .ThenInclude(s => s.ChildSubject)
                .ToListAsync();

            List<SubjectDTO> subjectsDTO = _mapper.Map<List<SubjectDTO>>(subjects);

            List<SubjectDTO> filteredDTO = subjectsDTO.Where(s => s.Children.Any()).ToList();

            return Ok(filteredDTO);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDTO>> GetSubject(int id)
        {

            Subject subject = await _context.Subject
                .Include(s => s.SubjectHeirarchyParentSubject)
                .ThenInclude(s => s.ChildSubject)
                .FirstOrDefaultAsync(s => s.SubjectId == id);

            SubjectDTO subjectDTO = _mapper.Map<SubjectDTO>(subject);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subjectDTO);
        }


        // GET: api/Subjects/5/Courses
        [HttpGet("{id}/Courses")]
        public async Task<ActionResult<SubjectDTO>> GetSubjectCourses(int id)
        {

            List<Subject> subject = await _context.Subject
                .Include(s => s.Course)
                    .ThenInclude(c => c.User)
                .Where(s => s.SubjectId == id)
                .ToListAsync();

            List<SubjectCourseDTO> subjectDTO = _mapper.Map<List<SubjectCourseDTO>>(subject);
            if (subject == null)
            {
                return NotFound();
            }

            return Ok(subjectDTO);
        }

        // PUT: api/Subjects/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubject(int id, Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return BadRequest();
            }

            _context.Entry(subject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectExists(id))
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

        // POST: api/Subjects
        [HttpPost]
        public async Task<ActionResult<Subject>> PostSubject(Subject subject)
        {
            _context.Subject.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubject", new { id = subject.SubjectId }, subject);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subject>> DeleteSubject(int id)
        {
            var subject = await _context.Subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _context.Subject.Remove(subject);
            await _context.SaveChangesAsync();

            return subject;
        }

        private bool SubjectExists(int id)
        {
            return _context.Subject.Any(e => e.SubjectId == id);
        }
    }
}
