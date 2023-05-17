using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int Id { get; set; }
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<Livre> Livres { get; set; }
}