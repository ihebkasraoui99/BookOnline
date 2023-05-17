using System.ComponentModel.DataAnnotations;

public class LivreViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
    public string Nom { get; set; }

    [Required(ErrorMessage = "Le champ Category est obligatoire.")]
    public string Category { get; set; }

    [Display(Name = "Auteur")]
    [Required(ErrorMessage = "Le champ Auteur est obligatoire.")]
    public int AuthorId { get; set; }

    [Display(Name = "Client")]
    [Required(ErrorMessage = "Le champ Client est obligatoire.")]
    public int ClientId { get; set; }

    public IEnumerable<Author> Authors { get; set; }

    public IEnumerable<Client> Clients { get; set; }
}
