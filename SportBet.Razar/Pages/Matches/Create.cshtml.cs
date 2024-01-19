using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Matches
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
        ViewData["AwayTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
        ViewData["HomeTeamId"] = new SelectList(_context.Teams, "TeamId", "TeamId");
            return Page();
        }

        [BindProperty]
        public Match Match { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Matches.Add(Match);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
