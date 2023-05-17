using System.ComponentModel.DataAnnotations;

public class LivreCreateViewModel
{
    [Required(ErrorMessage = "Le nom du livre est requis.")]
    [Display(Name = "Nom")]
    public string Nom { get; set; }

    [Required(ErrorMessage = "La catégorie du livre est requise.")]
    [Display(Name = "Category")]
    public string Category { get; set; }

    [Display(Name = "Auteur")]
    public Author SelectedAuthor { get; set; }
[Display(Name = "Auteur")]
public int SelectedAuthorId { get; set; }

    public List<Author> Authors { get; set; }

    [Display(Name = "Client")]
    public int ClientId { get; set; }
    public List<Client> Clients { get; set; }
    public List<AuthorViewModel> AuthorList { get; set; }
}
