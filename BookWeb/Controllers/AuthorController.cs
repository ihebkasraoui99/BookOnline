using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class AuthorController : Controller
{
    private readonly ApplicationDbContext _context;

    public AuthorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Author
    public async Task<IActionResult> Index()
    {
        var authors = await _context.Authors.ToListAsync();
        return View(authors);
    }

    // GET: Author/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    // GET: Author/Create
    public IActionResult Create()
    {
        var authorViewModel = new AuthorViewModel();
        return View(authorViewModel);
    }


    // POST: Author/Create
    // POST: Author/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom")] Author author)
    {
        try
        {
            _context.Add(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "An error occurred while saving the author to the database.");
            // Log the exception details for debugging
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }


        return View(author);
    }


    // GET: Author/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

        if (author == null)
        {
            return NotFound();
        }

        return View(author);
    }
    private bool AuthorExists(int id)
    {
        return _context.Authors.Any(a => a.Id == id);
    }

    // POST: Author/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom")] Author author)
    {
        if (id != author.Id)
        {
            return NotFound();
        }


        try
        {
            _context.Update(author);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorExists(author.Id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));


        return View(author);
    }
    public IActionResult Delete(int id)
    {
        var author = _context.Authors.FirstOrDefault(a => a.Id == id);
        if (author == null)
        {
            // Handle the case when the author with the provided ID doesn't exist
            return RedirectToAction("Index");
        }

        _context.Authors.Remove(author);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
