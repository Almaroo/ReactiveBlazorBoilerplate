using BlazorAppBoilerplate.Data;

namespace BlazorAppBoilerplate.ApiClients;

public class CivilizationSearchResult
{
    public List<Civilization> Civilizations { get; set; } = new();
}