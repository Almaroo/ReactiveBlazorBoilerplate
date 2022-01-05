using BlazorAppBoilerplate.Data;
using DynamicData;

namespace BlazorAppBoilerplate.Services;

public interface IAgeOfEmpiresService
{
    IObservable<IChangeSet<Civilization, int>> GetCivilizations();
}