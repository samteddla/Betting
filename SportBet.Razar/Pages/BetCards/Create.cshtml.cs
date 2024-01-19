using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.BetCards
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
        ViewData["MatchSelectionId"] = new SelectList(_context.MatchSelections, "MatchSelectionId", "MatchSelectionId");
        ViewData["MatchTypeId"] = new SelectList(_context.BetMatchTypes, "MatchTypeId", "MatchTypeId");
        ViewData["PersonId"] = new SelectList(_context.People, "PersonId", "PersonId");
            return Page();
        }

        [BindProperty]
        public BetCard BetCard { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.BetCards.Add(BetCard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
