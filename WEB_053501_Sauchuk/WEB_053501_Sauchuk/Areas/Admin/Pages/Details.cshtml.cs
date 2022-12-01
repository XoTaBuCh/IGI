using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_053501_Sauchuk.Data;
using WEB_053501_Sauchuk.Entities;

namespace WEB_053501_Sauchuk.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_053501_Sauchuk.Data.ApplicationDbContext _context;

        public DetailsModel(WEB_053501_Sauchuk.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Food Food { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Foods == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }
            else 
            {
                Food = food;
            }
            return Page();
        }
    }
}
