namespace Jellyfin.Plugin.Extractor.Services
{
  public interface IStudioExtractorService
  {
    string extractStudio(string movie);
    string removeSurroundingBraces(string movie);
  }
}