using Jellyfin.Plugin.Extractor.Services;
using Xunit;

namespace Jellyfin.Plugin.Extractor.Tests.Services
{
  public class StudioExtractorServiceTest
  {
    private IStudioExtractorService studioExtractorService;

    public StudioExtractorServiceTest()
    {
      this.studioExtractorService = new StudioExtractorService();
    }

    [Fact]
    public void Constructor_CanBeConstructedWithNoArgs()
    {
      //When
      this.studioExtractorService = new StudioExtractorService();

      //Then
      Assert.NotNull(this.studioExtractorService);
    }

    [Fact]
    public void Constructor_CanBeConstructedWithOpenClosingChars()
    {
      //When
      this.studioExtractorService = new StudioExtractorService('[', ']');

      //Then
      Assert.NotNull(this.studioExtractorService);
    }

    [Fact]
    public void extractStudio_ShouldReturnNull()
    {
      //Given
      var movie = "MovieName - actor";

      //When
      var studio = this.studioExtractorService.extractStudio(movie);

      //Then
      Assert.Null(studio);
    }

    [Fact]
    public void extractStudio_ShouldReturnStudio()
    {
      //Given
      var movie = "[Studio] MovieName - actor";

      //When
      var studio = this.studioExtractorService.extractStudio(movie);

      //Then
      Assert.Equal("Studio", studio);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnMovie()
    {
      //Given
      var movie = "Movie - actor";

      //When
      var updatedMovie = this.studioExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal(movie, updatedMovie);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnMovieWithoutBraces_WhenStudioIsInFront()
    {
      //Given
      var movie = "[Studio] Movie - actor";

      //When
      var updatedMovie = this.studioExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("Studio Movie - actor", updatedMovie);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnMovieWithoutBraces_WhenStudioInMiddle()
    {
      //Given
      var movie = "Movie [Studio] - actor";

      //When
      var updatedMovie = this.studioExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("Movie Studio - actor", updatedMovie);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnMovieWithoutBraces_WhenStudioIsAtTheEnd()
    {
      //Given
      var movie = "Movie - actor [Studio]";

      //When
      var updatedMovie = this.studioExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("Movie - actor Studio", updatedMovie);
    }
  }
}