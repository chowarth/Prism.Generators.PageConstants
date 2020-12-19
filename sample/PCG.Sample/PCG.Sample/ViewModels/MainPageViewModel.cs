using Prism.Generated;
using Prism.Navigation;
using System.Threading.Tasks;

namespace PCG.Sample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }

        protected override Task NavigateToAsync()
        {
            return NavigationService.NavigateAsync(PageConstants.PageA);
        }
    }
}
