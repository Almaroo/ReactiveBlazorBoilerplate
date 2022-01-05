using System.Reactive.Linq;
using BlazorAppBoilerplate.ApiClients;
using BlazorAppBoilerplate.Data;
using DynamicData;

namespace BlazorAppBoilerplate.Services;

public class AgeOfEmpiresService : IAgeOfEmpiresService, IDisposable
{
    private readonly IAgeOfEmpiresApiClient _ageOfEmpiresApiClient;

    private readonly SourceCache<Civilization, int> _queryResults = new(x => x.Id);
        
    private readonly Func<Exception, IObservable<CivilizationSearchResult>> _queryException =
        _ => Observable.Empty<CivilizationSearchResult>();

    public AgeOfEmpiresService(IAgeOfEmpiresApiClient ageOfEmpiresApiClient) =>
        _ageOfEmpiresApiClient = ageOfEmpiresApiClient;

    public IObservable<IChangeSet<Civilization, int>> GetCivilizations() => Observable
        .Create<IChangeSet<Civilization, int>>(
            observer =>
                _ageOfEmpiresApiClient
                    .GetCivilizations()
                    .Catch(_queryException)
                    .Select(x => x.Civilizations)
                    .Cache(_queryResults, false)
                    .Subscribe(observer));
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
        
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _queryResults?.Dispose();
        }
    }
}