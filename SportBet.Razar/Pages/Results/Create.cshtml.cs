using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Results
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
        ViewData["MatchId"] = new SelectList(_context.Matches, "MatchId", "MatchId");
        ViewData["MatchSelectionId"] = new SelectList(_context.MatchSelections, "MatchSelectionId", "Name");
        ViewData["MatchTypeId"] = new SelectList(_context.BetMatchTypes, "MatchTypeId", "Name");
            return Page();
        }

        [BindProperty]
        public BetResult BetResult { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BetResults.Add(BetResult);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
