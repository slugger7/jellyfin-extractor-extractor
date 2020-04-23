using Xunit;
using Jellyfin.Plugin.Extractor.Services;
using System.Collections.Generic;

namespace Jellyfin.Plugin.Extractor.Tests
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

    [Fact]
    public void extractGenres_ShouldReturnGenres()
    {
      //Given
      var movie = "Moviename - actor - (genre, genre2)";

      //When
      var genres = genreExtractorService.extractGenres(movie);

      //Then
      Assert.Equal(new List<string>() { "genre", "genre2" }, genres);
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
    public void removeGenresFromMovieName_ShouldReturnAMovieWithoutGenres()
    {
    //Given
    var movie = "MovieName - actor (genre1, genre3)";
    
    //When
    var updatedMovie = genreExtractorService.removeGenresFromMovie(movie);
    
    //Then
    Assert.Equal("MovieName - actor", updatedMovie);
    }
  }
}
