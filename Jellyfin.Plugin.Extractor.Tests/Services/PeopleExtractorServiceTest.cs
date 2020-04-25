using Xunit;
using System;
using Jellyfin.Plugin.Extractor.Services;
using System.Collections.Generic;

namespace Jellyfin.Plugin.Extractor.Tests.Services
{
  public class PeopleExtractorServiceTest
  {
    IPeopleExtractorService peopleExtractorService;

    public PeopleExtractorServiceTest()
    {
      this.peopleExtractorService = new PeopleExtractorService();
    }

    [Fact]
    public void Constructor_ShouldBeConstructedWithNoArgs()
    {
      //When
      var service = new PeopleExtractorService();

      //Then
      Assert.NotNull(service);
    }

    [Fact]
    public void Constructor_ShouldBeConstructedWithArgs()
    {
      //When
      var service = new PeopleExtractorService('{', '}', ',');

      //Then
      Assert.NotNull(service);
    }

    [Fact]
    public void extractPeople_ShouldReturnListOfStrings()
    {
      //Given
      var movie = "Movie (genre)";

      //When
      var result = this.peopleExtractorService.extractPeople(movie);

      //Then
      Assert.True(result is IEnumerable<string>);
    }

    [Fact]
    public void extractPeople_ShouldReturnAListWithOnePerson()
    {
      //Given
      var movie = "Movie {actor} (genre)";

      //When
      var result = this.peopleExtractorService.extractPeople(movie);

      //Then
      Assert.Equal(new List<string>() { "actor" }, result);
    }

    [Fact]
    public void extractPeople_ShouldReturnAListWithMultiplePeople()
    {
      //Given
      var movie = "Movie {actor1, actor2} (genre)";

      //When
      var result = this.peopleExtractorService.extractPeople(movie);

      //Then
      Assert.Equal(new List<string>() { "actor1", "actor2" }, result);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnOriginalString()
    {
      //Given
      var movie = "Movie (genre)";

      //When
      var result = this.peopleExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal(movie, result);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnStringWithoutBraces_GivenBracesAreAtFront()
    {
      //Given
      var movie = "{actor} Movie (genre)";

      //When
      var result = this.peopleExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("actor Movie (genre)", result);
    }

    [Fact]
    public void removeSurroundingBraces_ShouldReturnStringWithoutBraces_GivenBracesAreAtMiddle()
    {
      //Given
      var movie = "Movie {actor} (genre)";

      //When
      var result = this.peopleExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("Movie actor (genre)", result);
    }
    
    [Fact]
    public void removeSurroundingBraces_ShouldReturnStringWithoutBraces_GivenBracesAreAtEnd()
    {
      //Given
      var movie = "Movie {actor}";

      //When
      var result = this.peopleExtractorService.removeSurroundingBraces(movie);

      //Then
      Assert.Equal("Movie actor", result);
    }
  }
}