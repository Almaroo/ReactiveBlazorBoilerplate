using Refit;

namespace BlazorAppBoilerplate.ApiClients;

public interface IAgeOfEmpiresApiClient
{
    [Get("/civilizations")]
    IObservable<CivilizationSearchResult> GetCivilizations();
}