using System.Collections.Generic;

namespace Jellyfin.Plugin.Extractor.Services
{
  public interface IPeopleExtractorService
  {
      IEnumerable<string> extractPeople(string movieName);
      string removeSurroundingBraces(string movieName);
  }
}