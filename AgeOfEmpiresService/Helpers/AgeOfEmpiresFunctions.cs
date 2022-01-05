using System.Reactive.Linq;
using BlazorAppBoilerplate.Data;
using DynamicData;

namespace BlazorAppBoilerplate.Services;

public static class AgeOfEmpiresFunctions
{
    public static IObservable<IChangeSet<Civilization, int>> Cache(
        this IObservable<IEnumerable<Civilization>> result,
        SourceCache<Civilization, int> cache,
        bool clearCache
    ) => result
        .Do(UpdateCache(cache, clearCache))
        .Select(_ => cache.Connect().RefCount())
        .Switch();
        
    private static Action<IEnumerable<Civilization>> UpdateCache(ISourceCache<Civilization, int> cache, bool clearCache) =>
        queryResults =>
        {
            if (clearCache)
            {
                cache.Edit(updater =>
                {
                    updater.Clear();
                    updater.AddOrUpdate(queryResults);
                });
            }
            else
            {
                cache.EditDiff(queryResults, (first, second) => first.Id == second.Id);
            }
        };
}