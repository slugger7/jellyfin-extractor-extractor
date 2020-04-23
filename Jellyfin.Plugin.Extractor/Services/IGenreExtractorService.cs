using System.Collections.Generic;

namespace Jellyfin.Plugin.Extractor.Services
{
  public interface IGenreExtractorService
  {
      IEnumerable<string> extractGenres(string movieName);
      string removeGenresFromMovie(string movieName);
  }
}