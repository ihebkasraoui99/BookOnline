using System.ComponentModel.DataAnnotations;

public class Author
{
    [Key]
    public int Id { get; set; }
    public string Nom { get; set; }
    public virtual ICollection<Livre> Livres { get; set; }
}