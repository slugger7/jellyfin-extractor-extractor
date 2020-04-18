using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using Microsoft.Extensions.Logging;

namespace Jellyfin.Plugin.Extractor
{
  public class PostScanTask : ILibraryPostScanTask
  {
    private ILibraryManager libraryManager;
    private ILogger logger;
    public PostScanTask(ILibraryManager libraryManager, ILogger logger)
    {
      this.libraryManager = libraryManager;
      this.logger = logger;
    }

    private IEnumerable<string> extractGenres(string movieName)
    {
      int openIndex = movieName.LastIndexOf("(");
      int closedIndex = movieName.LastIndexOf(")");
      var genres = movieName
        .Substring(openIndex + 1, closedIndex - openIndex - 1).Split(',').Select(genre => genre.Trim());
      return genres;
    }

    private string removeGenresFromMovie(string movieName) => movieName.Substring(0, movieName.LastIndexOf(" ("));

    private void addGenresToMovie(BaseItem movie, IEnumerable<string> genres)
    {
      foreach (var genre in genres)
      {
        if (!movie.Genres.Contains(genre))
        {
          movie.AddGenre(genre);
        }
      }
    }

    private void updateMovie(BaseItem movie, CancellationToken token)
    {
      var genres = extractGenres(movie.Name);
      var existingGenres = movie.Genres;

      addGenresToMovie(movie, genres);

      movie.Name = removeGenresFromMovie(movie.Name);

      movie.UpdateToRepository(ItemUpdateType.MetadataEdit, token);
    }

    private IEnumerable<BaseItem> getMovies() => libraryManager.GetItemList(new InternalItemsQuery()
    {
      IncludeItemTypes = new string[] { "movie" }
    });

    public Task Run(IProgress<double> progress,
                    CancellationToken cancellationToken)
    {
      logger.LogInformation("Running extraction task post library scan");

      var movies = getMovies();

      foreach (var movie in movies)
      {
        updateMovie(movie, cancellationToken);
      }

      logger.LogInformation("Running extraction complete");
      return Task.CompletedTask;
    }
  }
}