using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Rewards
{
    public class EditModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public EditModel(SportBet.Razar.Model.BetOnContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Reward Reward { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward =  await _context.Rewards.FirstOrDefaultAsync(m => m.RewardId == id);
            if (reward == null)
            {
                return NotFound();
            }
            Reward = reward;
           ViewData["BetCardId"] = new SelectList(_context.BetCards, "BetCardId", "BetCardId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Reward).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RewardExists(Reward.RewardId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RewardExists(int id)
        {
            return _context.Rewards.Any(e => e.RewardId == id);
        }
    }
}
