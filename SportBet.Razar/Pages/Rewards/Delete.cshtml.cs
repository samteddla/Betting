using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SportBet.Razar.Model;

namespace SportBet.Razar.Pages.Rewards
{
    public class DeleteModel : PageModel
    {
        private readonly SportBet.Razar.Model.BetOnContext _context;

        public DeleteModel(SportBet.Razar.Model.BetOnContext context)
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

            var reward = await _context.Rewards.FirstOrDefaultAsync(m => m.RewardId == id);

            if (reward == null)
            {
                return NotFound();
            }
            else
            {
                Reward = reward;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reward = await _context.Rewards.FindAsync(id);
            if (reward != null)
            {
                Reward = reward;
                _context.Rewards.Remove(Reward);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
