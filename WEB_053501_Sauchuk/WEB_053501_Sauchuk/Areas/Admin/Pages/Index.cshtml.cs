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
    public class IndexModel : PageModel
    {
        private readonly WEB_053501_Sauchuk.Data.ApplicationDbContext _context;

        public IndexModel(WEB_053501_Sauchuk.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Food> Food { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Foods != null)
            {
                Food = await _context.Foods
                .Include(f => f.Category)
                .Include(f => f.Image).ToListAsync();
            }
        }
    }
}
