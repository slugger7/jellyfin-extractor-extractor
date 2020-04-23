using System;

namespace Jellyfin.Plugin.Extractor.Services
{
  public class StudioExtractorService : IStudioExtractorService
  {
    private char studioOpenChar;
    private char studioClosingChar;

    public StudioExtractorService() 
    {
      this.studioOpenChar = '[';
      this.studioClosingChar = ']';
    }
    public StudioExtractorService(char studioOpenChar, char studioClosingChar)
    {
      this.studioOpenChar = studioOpenChar;
      this.studioClosingChar = studioClosingChar;
    }

    public string extractStudio(string movie)
    {
      var openIndex = movie.IndexOf(studioOpenChar);
      var closeIndex = movie.IndexOf(studioClosingChar);
      if (openIndex >= 0 && closeIndex > 0)
      {
        return movie.Substring(openIndex + 1, closeIndex - openIndex - 1).Trim();
      }
      return null;
    }

    public string removeSurroundingBraces(string movie)
    {
      var openIndex = movie.IndexOf(studioOpenChar);
      var closeIndex = movie.IndexOf(studioClosingChar);
      if (openIndex >= 0 && closeIndex > 0)
      {
        var before = movie.Substring(0, openIndex);
        var studio = movie.Substring(openIndex + 1, closeIndex - openIndex - 1);
        var after = movie.Substring(closeIndex + 1);
        movie = $"{before}{studio}{after}";
      }
      return movie;
    }
  }
}