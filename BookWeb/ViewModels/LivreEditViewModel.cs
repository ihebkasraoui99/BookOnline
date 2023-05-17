using System.ComponentModel.DataAnnotations;

public class LivreEditViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Nom is required.")]
    public string Nom { get; set; }

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; }

    [Display(Name = "Author")]
    public int SelectedAuthorId { get; set; }

    public List<Author> Authors { get; set; }
}
