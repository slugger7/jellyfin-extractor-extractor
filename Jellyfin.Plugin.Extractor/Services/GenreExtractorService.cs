using System.Collections.Generic;
using System.Linq;

namespace Jellyfin.Plugin.Extractor.Services
{
  public class GenreExtractorService : IGenreExtractorService
  {
    private char genreOpenChar;
    private char genreClosingChar;
    private char genreDelimiter;

    public GenreExtractorService()
    {
      this.genreOpenChar = '(';
      this.genreClosingChar = ')';
      this.genreDelimiter = ',';
    }
    public GenreExtractorService(char genreOpenChar, char genreClosingChar, char genreDelimiter)
    {
      this.genreOpenChar = genreOpenChar;
      this.genreClosingChar = genreClosingChar;
      this.genreDelimiter = genreDelimiter;
    }

    public IEnumerable<string> extractGenres(string movieName)
    {
      int openIndex = movieName.IndexOf(genreOpenChar);
      int closedIndex = movieName.IndexOf(genreClosingChar);
      if (openIndex >= 0 && closedIndex > 0)
      {
        return movieName
        .Substring(openIndex + 1, closedIndex - openIndex - 1).Split(genreDelimiter).Select(genre => genre.Trim());
      }

      return new List<string>();
    }

    public string removeGenresFromMovie(string movieName)
    {
      var openIndex = movieName.IndexOf(genreOpenChar);
      var closedIndex = movieName.IndexOf(genreClosingChar);
      if (openIndex >= 0 && closedIndex > 0)
      {
        var before = movieName.Substring(0, openIndex).Trim();
        var after = movieName.Substring(closedIndex + 1).Trim();
        movieName = $"{before} {after}".Trim();
      }
      return movieName;
    }
  }
}