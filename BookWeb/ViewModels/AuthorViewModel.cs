using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class AuthorViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Author Nom")]
    public string Nom { get; set; }

    public List<LivreViewModel> Livres { get; set; }
}
