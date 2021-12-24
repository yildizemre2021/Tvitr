using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tvitr.Data;
using Tvitr.Models;
using Tvitr.Models.ViewModels;

namespace Tvitr.Controllers
{
    public class TweetController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TweetController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Tweet
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(await _context.Tweet.Where(t => t.User.Email == User.Identity.Name).OrderByDescending(d => d.Id).ToListAsync());
            }
            else
            {
                return Redirect("Identity/Account/Login");
            }
        }

        public async Task<IActionResult> ListUsers()
        {
            var users = _userManager.Users.ToList();
            var model = new TweetViewModel();
            var models = new List<TweetViewModel>();
            var emails = new List<string>();
            foreach (var user in users)
            {
                model.Email = user.Email;
                models.Add(model);
            }

            return View(models);
        }

        // GET Tweet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet
            .FirstOrDefaultAsync(m => m.Id == id);
            if (tweet == null)
                return NotFound();

            return View(tweet);
        }

        // GET: Tweet/Create
        public IActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        //POST: Tweet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,ImageUrl,User,UserId")] Tweet tweet)
        {

            string email = User.Identity.Name.ToString();
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            tweet.User = user;
            tweet.UserId = user.Id;

            _context.Add(tweet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            if (tweet == null)
                return View(tweet);
        }

        // GET: Tweet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet.FindAsync(id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // POST: Tweet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Text,ImageUrl")] Tweet tweet)
        {
            if (id != tweet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tweet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TweetExists(tweet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tweet);
        }

        private bool TweetExists(int id)
        {
            return _context.Tweet.Any(e => e.Id == id);
        }

        // GET: Tweet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tweet = await _context.Tweet
            .FirstOrDefaultAsync(m => m.Id == id);
            if (tweet == null)
            {
                return NotFound();
            }

            return View(tweet);
        }

        // POST: Tweet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tweet = await _context.Tweet.FindAsync(id);
            _context.Tweet.Remove(tweet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

