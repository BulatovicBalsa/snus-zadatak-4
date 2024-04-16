namespace Zadatak4;

internal class DirectorHandler(ImdbContext db)
{
    public void AddDirector(Director director)
    {
        db.Directors.Add(director);
        db.SaveChanges();
    }

    public void UpdateDirector(Director director)
    {
        var directorToUpdate = GetDirector(director.Id);
        if (directorToUpdate == null) return;

        directorToUpdate.Name = director.Name;
        directorToUpdate.Oscars = director.Oscars;
        directorToUpdate.Born = director.Born;
        directorToUpdate.Died = director.Died;

        db.SaveChanges();
    }

    public void DeleteDirector(int id)
    {
        var director = GetDirector(id) ?? throw new KeyNotFoundException($"Director with id {id} not found");

        db.Directors.Remove(director);
        db.SaveChanges();
    }

    public List<Director> GetDirectors()
    {
        return [.. db.Directors];
    }

    public Director? GetDirector(int id)
    {
        return db.Directors.FirstOrDefault(d => d.Id == id);
    }
}