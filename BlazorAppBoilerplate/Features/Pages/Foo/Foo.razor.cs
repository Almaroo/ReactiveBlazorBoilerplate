using System.Reactive.Linq;
using ReactiveUI;

namespace BlazorAppBoilerplate.Features.Pages.Foo;

public partial class Foo
{
    public Foo()
    {
        this.WhenAnyObservable(x => x._viewModel.Changed)
            .Throttle(TimeSpan.FromMilliseconds(500), RxApp.MainThreadScheduler)
            .Subscribe(_ => StateHasChanged());
    }

    protected override void OnInitialized()
    {
        ViewModel = _viewModel;
    }
}