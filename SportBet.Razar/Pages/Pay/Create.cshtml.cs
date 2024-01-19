using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Pay
{
    public class CreateModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public CreateModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["BetCardId"] = new SelectList(_context.BetCards, "BetCardId", "BetCardId");
            return Page();
        }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Payments.Add(Payment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
