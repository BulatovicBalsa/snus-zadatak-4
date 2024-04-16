namespace Zadatak4;

internal class MovieHandler(ImdbContext db)
{
    public void AddMovie(Movie movie)
    {
        db.Movies.Add(movie);
        db.SaveChanges();
    }

    public void UpdateMovie(Movie movie)
    {
        var movieToUpdate = GetMovie(movie.Id);
        if (movieToUpdate == null) return;

        movieToUpdate.Title = movie.Title;
        movieToUpdate.Rating = movie.Rating;
        movieToUpdate.Oscars = movie.Oscars;
        movieToUpdate.Year = movie.Year;
        movieToUpdate.DirectorId = movie.DirectorId;

        db.SaveChanges();
    }

    public void DeleteMovie(int id)
    {
        var movie = GetMovie(id) ?? throw new KeyNotFoundException($"Movie with id {id} not found");

        db.Movies.Remove(movie);
        db.SaveChanges();
    }

    public List<Movie> GetMovies()
    {
        return [.. db.Movies];
    }

    public Movie? GetMovie(int id)
    {
        return db.Movies.Find(id);
    }
}