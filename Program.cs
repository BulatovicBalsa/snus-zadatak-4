using Zadatak4;

const string xmlPath = "../../../Sets.xml";

using var db = new ImdbContext();
XmlReader.LoadXml(xmlPath, db);

var queryReport = new QueryReport(db);

Console.WriteLine("Report 1:");
queryReport.MoviesByDirector();
Console.WriteLine();

Console.WriteLine("Report 2:");
queryReport.DirectorFullNameForEachMovie();
Console.WriteLine();

Console.WriteLine("Report 3:");
queryReport.BestRatedMovies(3);
Console.WriteLine();

Console.WriteLine("Report 4:");
queryReport.DirectorsWithOscarsSum();
Console.WriteLine();

Console.WriteLine("Report 5:");
queryReport.DirectorsWithMoviesWithMoreThen2Oscars();
Console.WriteLine();

Console.WriteLine("Report 6:");
queryReport.AliveDirectorsWithMoreThen2Oscars();
