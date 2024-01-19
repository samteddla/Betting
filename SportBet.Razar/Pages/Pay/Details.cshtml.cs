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
    public class DetailsModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DetailsModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        public Payment Payment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            else
            {
                Payment = payment;
            }
            return Page();
        }
    }
}
