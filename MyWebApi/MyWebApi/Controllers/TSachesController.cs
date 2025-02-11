using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TSachesController : ControllerBase
    {
        private readonly QlthuVienContext _context;

        public TSachesController(QlthuVienContext context)
        {
            _context = context;
        }

        // GET: api/TSaches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TSach>>> GetTSaches()
        {
            return await _context.TSaches.ToListAsync();
        }

        // GET: api/TSaches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TSach>> GetTSach(string id)
        {
            var tSach = await _context.TSaches.FindAsync(id);

            if (tSach == null)
            {
                return NotFound();
            }

            return tSach;
        }

        // PUT: api/TSaches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTSach(string id, TSach tSach)
        {
            if (id != tSach.MaSach)
            {
                return BadRequest();
            }

            _context.Entry(tSach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TSachExists(id))
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

        // POST: api/TSaches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TSach>> PostTSach(TSach tSach)
        {
            _context.TSaches.Add(tSach);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TSachExists(tSach.MaSach))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTSach", new { id = tSach.MaSach }, tSach);
        }

        // DELETE: api/TSaches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTSach(string id)
        {
            var tSach = await _context.TSaches.FindAsync(id);
            if (tSach == null)
            {
                return NotFound();
            }

            _context.TSaches.Remove(tSach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TSachExists(string id)
        {
            return _context.TSaches.Any(e => e.MaSach == id);
        }
    }
}
