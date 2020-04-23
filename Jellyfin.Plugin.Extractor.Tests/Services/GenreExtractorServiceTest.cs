using Xunit;
using Jellyfin.Plugin.Extractor.Services;
using System.Collections.Generic;

namespace Jellyfin.Plugin.Extractor.Tests.Services
{
  public class GenreExtractorServiceTests
  {
    private readonly IGenreExtractorService genreExtractorService;

    public GenreExtractorServiceTests()
    {
      this.genreExtractorService = new GenreExtractorService();
    }

    [Fact]
    public void extractGenres_ShouldReturnAListOfStrings()
    {
      //Given
      var movie = "MovieName - actor";

      //When
      var result = this.genreExtractorService.extractGenres(movie);

      //Then
      Assert.True(result is IEnumerable<string>);
    }

    [Fact]
    public void extractGenres_ShouldReturnAGenre()
    {
      //Given
      var movie = "MovieName - actor - (genre)";
      //When
      var genres = genreExtractorService.extractGenres(movie);

      //Then
      Assert.Equal(new List<string>() { "genre" }, genres);
    }

    [Theory]
    [InlineData("Moviename - actor - (genre, genre)")]
    [InlineData("(genre, genre) Movie - actor")]
    [InlineData("Movie (genre, genre) - actor")]
    public void extractGenres_ShouldReturnGenres(string movie)
    {
      //When
      var genres = genreExtractorService.extractGenres(movie);

      //Then
      Assert.Equal(new List<string>() { "genre", "genre" }, genres);
    }

    [Fact]
    public void removeGenresFromMovieName_ShouldReturnMovieWithNoAlterations()
    {
      //Given
      var movie = "MovieName - actor";

      //When
      var updatedMovie = genreExtractorService.removeGenresFromMovie(movie);

      //Then
      Assert.Equal(movie, updatedMovie);
    }

    [Fact]
    public void removeGenresFromMovieName_ShouldReturnAMovieWithoutGenres_WhenGenresAtEnd()
    {
      //Given
      var movie = "MovieName - actor (genre1, genre3)";

      //When
      var updatedMovie = genreExtractorService.removeGenresFromMovie(movie);

      //Then
      Assert.Equal("MovieName - actor", updatedMovie);
    }

    [Fact]
    public void removeGenresFromMovieName_ShouldReturnAMovieWithoutGenres_WhenGenresAtMiddle()
    {
      //Given
      var movie = "MovieName (genre1, genre3) actor";

      //When
      var updatedMovie = genreExtractorService.removeGenresFromMovie(movie);

      //Then
      Assert.Equal("MovieName actor", updatedMovie);
    }

    [Fact]
    public void removeGenresFromMovieName_ShouldReturnAMovieWithoutGenres_WhenGenresAtStart()    {
      //Given
      var movie = "(genre1, genre3) MovieName - actor";

      //When
      var updatedMovie = genreExtractorService.removeGenresFromMovie(movie);

      //Then
      Assert.Equal("MovieName - actor", updatedMovie);
    }
  }
}
