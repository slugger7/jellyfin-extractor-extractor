using System.Collections.Generic;
using System.Linq;

namespace Jellyfin.Plugin.Extractor.Services
{
  public class PeopleExtractorService : IPeopleExtractorService
  {
    private char openChar;
    private char closeChar;
    private char delimeter;

    public PeopleExtractorService()
    {
      this.openChar = '{';
      this.closeChar = '}';
      this.delimeter = ',';
    }
    public PeopleExtractorService(char openChar, char closeChar, char delimeter)
    {
      this.openChar = openChar;
      this.closeChar = closeChar;
      this.delimeter = delimeter;
    }

    public IEnumerable<string> extractPeople(string movieName)
    {
      int openIndex = movieName.IndexOf(this.openChar);
      int closedIndex = movieName.IndexOf(this.closeChar);

      if (openIndex >= 0 && closedIndex > 0)
      {
        return movieName
          .Substring(openIndex + 1, closedIndex - openIndex - 1)
          .Split(delimeter)
          .Select(person => person.Trim());
      }

      return new List<string>();
    }

    public string removeSurroundingBraces(string movieName)
    {
      var openIndex = movieName.IndexOf(openChar);
      var closedIndex = movieName.IndexOf(closeChar);

      if (openIndex >= 0 && closedIndex > 0)
      {
        var before = movieName.Substring(0, openIndex).Trim();
        var middle = movieName.Substring(openIndex + 1, closedIndex - openIndex -1).Trim();
        var after = movieName.Substring(closedIndex + 1).Trim();
        movieName = $"{before} {middle} {after}".Trim();
      }

      return movieName;
    }
  }
}