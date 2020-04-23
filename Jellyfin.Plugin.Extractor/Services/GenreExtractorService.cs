using System.Collections.Generic;
using System.Linq;

namespace Jellyfin.Plugin.Extractor.Services
{
  public class GenreExtractorService : IGenreExtractorService
  {
    public IEnumerable<string> extractGenres(string movieName)
    {
      int openIndex = movieName.IndexOf("(");
      int closedIndex = movieName.IndexOf(")");
      if (openIndex > 0 && closedIndex > 0)
      {
        return movieName
        .Substring(openIndex + 1, closedIndex - openIndex - 1).Split(',').Select(genre => genre.Trim());
      }

      return new List<string>();
    }

    public string removeGenresFromMovie(string movieName)
    {
      var openIndex = movieName.LastIndexOf(" (");
      if (openIndex > 0)
      {
        movieName = movieName.Substring(0, openIndex);
      }
      return movieName;
    }
  }
}