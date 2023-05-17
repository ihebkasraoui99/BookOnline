using System.ComponentModel.DataAnnotations;

public class Livre
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nom { get; set; }

    [Required]
    public string Category { get; set; }

    [Required]
    public int AuthorId { get; set; }

    [Required]
    public int ClientId { get; set; }

    public virtual Author Author { get; set; }

    public virtual Client Client { get; set; }
}