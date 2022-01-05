using System.Reactive.Linq;
using BlazorAppBoilerplate.Data;
using BlazorAppBoilerplate.Services;
using DynamicData;
using ReactiveUI;

namespace BlazorAppBoilerplate.Features.Pages.Foo
{
    public class FooViewModel : ReactiveObject
    {
        private readonly IAgeOfEmpiresService _ageOfEmpiresService;
        private string _bar;
        private IEnumerable<Civilization> _civilizations = Enumerable.Empty<Civilization>();
        private ObservableAsPropertyHelper<bool> _canClear;

        public FooViewModel(IAgeOfEmpiresService ageOfEmpiresService)
        {
            _ageOfEmpiresService = ageOfEmpiresService;
            
            this.WhenAnyValue(x => x.Bar)
                .Where(string.IsNullOrEmpty)
                .Skip(1)
                .Select(_ => Enumerable.Empty<Civilization>())
                .ToObservableChangeSet(x => x.Id)
                .DefaultIfEmpty()!
                .ToCollection()
                .Do(x => Console.WriteLine(x.Count))
                .BindTo(this, x => x.Civilizations);

            this.WhenAnyValue(x => x.Bar)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Trim())
                .SelectMany(GetCivilizations)
                .ToCollection()
                .Do(x => Console.WriteLine(x.Count))
                .BindTo(this, x => x.Civilizations);
        }
        
        public string Bar
        {
            get => _bar;
            set => this.RaiseAndSetIfChanged(ref _bar, value);
        }

        public IEnumerable<Civilization> Civilizations
        {
            get => _civilizations;
            set => this.RaiseAndSetIfChanged(ref _civilizations, value);
        }
        
        private IObservable<IChangeSet<Civilization, int>> GetCivilizations => _ageOfEmpiresService.GetCivilizations();
    }
}