namespace Zadatak4;

internal class QueryReport(ImdbContext db)
{
    public MovieHandler MovieHandler { get; } = new MovieHandler(db);
    public DirectorHandler DirectorHandler { get; } = new DirectorHandler(db);

    public void MoviesByDirector()
    {
        var directorsWithMovies = db.Movies
            .GroupBy(m => m.DirectorId)
            .Select(g => new
            {
                Director = db.Directors.FirstOrDefault(d => d.Id == g.Key),
                Movies = g
            });

        foreach (var directorWithMovies in directorsWithMovies)
        {
            Console.WriteLine(directorWithMovies.Director);
            foreach (var movie in directorWithMovies.Movies) Console.WriteLine($"  {movie}");
        }
    }


    public void DirectorFullNameForEachMovie()
    {
        var moviesWithDirector = db.Movies.Select(m => new
        {
            Movie = m,
            Director = db.Directors.FirstOrDefault(d => d.Id == m.DirectorId)
        }).Select(md => new
        {
            MovieTitle = md.Movie.Title,
            DirectorName = md.Director!.Name
        });

        foreach (var movieWithDirector in moviesWithDirector)
            Console.WriteLine($"{movieWithDirector.MovieTitle} - Directed by: {movieWithDirector.DirectorName}");
    }

    public void BestRatedMovies(int count)
    {
        var bestRatedMovies = db.Movies.OrderByDescending(m => m.Rating).Take(count);

        foreach (var movie in bestRatedMovies) Console.WriteLine(movie);
    }

    public void DirectorsWithOscarsSum()
    {
        var directorsWithOscarsSum = db.Directors.Select(d => new
        {
            Director = d,
            OscarsSum = db.Movies.Where(m => m.DirectorId == d.Id).Sum(m => m.Oscars)
        });

        foreach (var directorWithOscarsSum in directorsWithOscarsSum)
            Console.WriteLine($"{directorWithOscarsSum.Director} - Oscars: {directorWithOscarsSum.OscarsSum}");
    }

    public void DirectorsWithMoviesWithMoreThen2Oscars()
    {
        var directorsWithMoviesWithMoreThen2Oscars = db.Movies.GroupBy(m => m.DirectorId).Select(g => new
        {
            Director = db.Directors.FirstOrDefault(d => d.Id == g.Key),
            Movies = g.Where(m => m.Oscars > 2).ToList()
        }).Where(d => d.Movies.Count > 0).Select(d => new
        {
            d.Director,
            OscarCount = d.Movies.Sum(m => m.Oscars)
        });

        foreach (var directorWithMovies in directorsWithMoviesWithMoreThen2Oscars)
            Console.WriteLine($"{directorWithMovies.Director}, Oscars: {directorWithMovies.OscarCount}");
    }

    public void AliveDirectorsWithMoreThen2Oscars()
    {
        var aliveDirectorsWithMoreThen2Oscars = db.Directors.Where(d => d.Died == null).Select(d => new
        {
            Director = d,
            OscarsSum = db.Movies.Where(m => m.DirectorId == d.Id).Sum(m => m.Oscars)
        }).Where(d => d.OscarsSum > 2);

        foreach (var directorWithOscarsSum in aliveDirectorsWithMoreThen2Oscars)
            Console.WriteLine($"{directorWithOscarsSum.Director} - Oscars: {directorWithOscarsSum.OscarsSum}");
    }
}