using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ClientController : Controller
{
    private readonly ApplicationDbContext _context;

    public ClientController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(ClientViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Email == model.Email);

            if (existingClient == null)
            {
                var client = new Client
                {
                    Nom = model.Nom,
                    Email = model.Email,
                    Password = model.Password
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Un compte avec cet email existe déjà.");
            }
        }

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(ClientViewModel model)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Email == model.Email && c.Password == model.Password);

        if (client != null)
        {
            // Enregistrer l'ID du client dans le cookie de l'utilisateur pour garder sa session
            HttpContext.Session.SetInt32("ClientId", client.Id);

            return new RedirectResult(url: "/Livre/Index", permanent: true,
                             preserveMethod: true);
        }
        else
        {
            ModelState.AddModelError("", "Email ou mot de passe incorrect.");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        // Supprimer l'ID du client du cookie de l'utilisateur pour mettre fin à sa session
        HttpContext.Session.Remove("ClientId");

        return RedirectToAction("Login");
    }
}
