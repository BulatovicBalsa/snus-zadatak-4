using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Zadatak4
{
    internal class XmlReader
    {
        public static void LoadXml(string path, ImdbContext db)
        {
            var xdoc = XDocument.Load(path);
            var movies = xdoc.Descendants("Movie").Select(m => new Movie
            {
                Id = int.Parse(m.Attribute("id")!.Value),
                Title = m.Value,
                Rating = double.Parse(m.Attribute("rating")!.Value, CultureInfo.InvariantCulture),
                Year = int.Parse(m.Attribute("year")!.Value),
                Oscars = int.Parse(m.Attribute("oscars")!.Value),
                DirectorId = int.Parse(m.Attribute("directorId")!.Value)
            }).ToList();

            var directors = xdoc.Descendants("Director").Select(d => new Director
            {
                Id = int.Parse(d.Attribute("id")!.Value),
                Name = d.Value,
                Oscars = int.Parse(d.Attribute("oscars")!.Value),
                Born = DateTime.ParseExact(d.Attribute("born")!.Value, "d.M.yyyy.", CultureInfo.InvariantCulture),
                Died = string.IsNullOrWhiteSpace(d.Attribute("died")?.Value) ? null : DateTime.ParseExact(d.Attribute("died")!.Value, "d.M.yyyy.", CultureInfo.InvariantCulture)
            }).ToList();

            db.Movies.AddRange(movies);
            db.Directors.AddRange(directors);
            db.SaveChanges();
        }

        private static void EmptyDb(ImdbContext db)
        {
            var allDirectors = db.Directors.ToList();
            var allMovies = db.Movies.ToList();

            // Remove all movies
            foreach (var movie in allMovies)
            {
                db.Movies.Remove(movie);
            }

            // Remove all directors
            foreach (var director in allDirectors)
            {
                db.Directors.Remove(director);
            }

            // Save changes to the database
            db.SaveChanges();
        }
    }
}
