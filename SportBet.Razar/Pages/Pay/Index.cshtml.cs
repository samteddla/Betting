using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Pay
{
    public class IndexModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public IndexModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Payment = await _context.Payments
                .Include(p => p.BetCard).ToListAsync();
        }
    }
}
