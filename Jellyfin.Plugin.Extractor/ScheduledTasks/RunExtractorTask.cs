using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Library;
using MediaBrowser.Model.Entities;
using MediaBrowser.Model.Tasks;
using Microsoft.Extensions.Logging;
using Jellyfin.Plugin.Extractor.Services;

namespace Jellyfin.Plugin.Extractor
{
  public class RunExtractorTask : IScheduledTask
  {
    private ILibraryManager libraryManager;
    private ILogger logger;
    private IGenreExtractorService genreExtractorService;

    public string Name => "Run Extractor";

    public string Key => "1a060367-6322-42c7-a9ec-825c17f8ceff";

    public string Description => "Run the extractor method";

    public string Category => "Plugin";

    public RunExtractorTask(ILibraryManager libraryManager, ILogger logger)
    {
      this.libraryManager = libraryManager;
      this.logger = logger;
      this.genreExtractorService = new GenreExtractorService();
    }

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
      var genres = genreExtractorService.extractGenres(movie.Name);
      var existingGenres = movie.Genres;

      addGenresToMovie(movie, genres);

      movie.Name = genreExtractorService.removeGenresFromMovie(movie.Name);

      movie.UpdateToRepository(ItemUpdateType.MetadataEdit, token);
    }

    private IEnumerable<BaseItem> getMovies() => libraryManager.GetItemList(new InternalItemsQuery()
    {
      VideoTypes = new VideoType[] { VideoType.VideoFile }
    });

    public Task Execute(CancellationToken cancellationToken, IProgress<double> progress)
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

    public IEnumerable<TaskTriggerInfo> GetDefaultTriggers()
    {
      IEnumerable<TaskTriggerInfo> enumerable = new List<TaskTriggerInfo>();
      return enumerable;
    }
  }
}