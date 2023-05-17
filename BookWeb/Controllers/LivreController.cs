using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class LivreController : Controller
{
    private readonly ApplicationDbContext _context;

    public LivreController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(string searchString)
    {
        var clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
        {
            return RedirectToAction("Login", "Client");
        }

        var client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
        if (client == null)
        {
            // Handle the case when the client with the provided ID doesn't exist
            return RedirectToAction("Login", "Client");
        }

        var query = _context.Livres
            .Include(l => l.Author)
            .Where(l => l.ClientId == clientId);

        if (!string.IsNullOrEmpty(searchString))
        {
            query = query.Where(l => l.Nom.Contains(searchString));
        }

        var livres = query.ToList();

        var viewModel = new LivreIndexViewModel
        {
            Client = client,
            Livres = livres
        };

        return View(viewModel);
    }


    public IActionResult Create()
    {
        var clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
        {
            return RedirectToAction("Login", "Client");
        }

        var viewModel = new LivreCreateViewModel
        {
            ClientId = (int)clientId,
            Authors = _context.Authors.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(LivreCreateViewModel model)
    {
        
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Login", "Client");
            }

            var livre = new Livre
            {
                Nom = model.Nom,
                Category = model.Category,
                AuthorId = model.SelectedAuthorId,
                ClientId = (int)clientId
            };

            _context.Livres.Add(livre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        
        

        // Repopulate the Authors list
        model.Authors = _context.Authors.ToList();

        return View(model);
    }
    public IActionResult Edit(int id)
    {
        var clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
        {
            return RedirectToAction("Login", "Client");
        }

        var livre = _context.Livres.Include(l => l.Author).FirstOrDefault(l => l.Id == id && l.ClientId == clientId);
        if (livre == null)
        {
            // Handle the case when the livre with the provided ID doesn't exist or doesn't belong to the client
            return RedirectToAction("Index");
        }

        var viewModel = new LivreEditViewModel
        {
            Id = livre.Id,
            Nom = livre.Nom,
            Category = livre.Category,
            SelectedAuthorId = livre.AuthorId,
            Authors = _context.Authors.ToList()
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(LivreEditViewModel model)
    {
        
            var clientId = HttpContext.Session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Login", "Client");
            }

            var livre = _context.Livres.FirstOrDefault(l => l.Id == model.Id && l.ClientId == clientId);
            if (livre == null)
            {
                // Handle the case when the livre with the provided ID doesn't exist or doesn't belong to the client
                return RedirectToAction("Index");
            }

            livre.Nom = model.Nom;
            livre.Category = model.Category;
            livre.AuthorId = model.SelectedAuthorId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        

        // Repopulate the Authors list
        model.Authors = _context.Authors.ToList();

        return View(model);
    }

    public IActionResult Delete(int id)
    {
        var clientId = HttpContext.Session.GetInt32("ClientId");
        if (clientId == null)
        {
            return RedirectToAction("Login", "Client");
        }

        var livre = _context.Livres.FirstOrDefault(l => l.Id == id && l.ClientId == clientId);
        if (livre == null)
        {
            // Handle the case when the livre with the provided ID doesn't exist or doesn't belong to the client
            return RedirectToAction("Index");
        }

        _context.Livres.Remove(livre);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}


