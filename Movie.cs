using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Zadatak4;

public class Movie
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; }

    [Required] [Range(0, 10)] public double Rating { get; set; }

    [Required] [Range(0, int.MaxValue)] public int Oscars { get; set; }

    [Required] public int Year { get; set; }

    public int DirectorId { get; set; }

    public virtual Director Director { get; set; }

    public override string ToString()
    {
        return $"{Title} ({Year}) - Rating: {Rating}, Oscars: {Oscars}, Directed by: {Director?.Name}";
    }
}

public class Director
{
    [Key] public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Name { get; set; }

    [Required] [Range(0, int.MaxValue)] public int Oscars { get; set; }

    [Required] public DateTime Born { get; set; }

    public DateTime? Died { get; set; }

    public override string ToString()
    {
        return $"{Name} - Oscars: {Oscars}, Born: {Born}, Died: {Died}";
    }
}

public class ImdbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }
}