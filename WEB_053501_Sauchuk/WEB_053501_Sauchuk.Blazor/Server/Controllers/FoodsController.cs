using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;
using WEB_053501_Sauchuk.Some;

namespace WEB_053501_Sauchuk.Blazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Foods : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Foods(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Foods
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Food>>> GetFoods(int? categoryId)
        {
            if (_context.Foods == null)
            {
                return NotFound();
            }
            if (categoryId == null)
            {
                return await _context.Foods.Include(f => f.Category).ToListAsync();
            }
            else
            {
                return await _context.Foods.Where(f => f.CategoryId == categoryId).Include(f => f.Category)
                    .ToListAsync();
            }
        }

        [HttpGet("GetImage")]
        public async Task<IActionResult> GetImage(int imageId)
        {
            Image img = await _context.Images.FirstOrDefaultAsync(img => img.Id == imageId);

            return File(ImageConverter.Base64ToImage(img.Base64Image), "image/png");
        }

        // GET: api/Foods/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            food.Category = _context.Categories.FirstOrDefault(c => c.Id == food.CategoryId);

            if (food == null)
            {
                return NotFound();
            }

            return food;
        }

        // PUT: api/Foods/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFood(int id, Food food)
        {
            if (id != food.Id)
            {
                return BadRequest();
            }

            _context.Entry(food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
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

        // POST: api/Foods
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Food>> PostFood(Food food)
        {
            if (_context.Foods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Foods'  is null.");
            }

            _context.Foods.Add(food);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFood", new { id = food.Id }, food);
        }

        // DELETE: api/Foods/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            if (_context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            _context.Foods.Remove(food);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodExists(int id)
        {
            return (_context.Foods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}