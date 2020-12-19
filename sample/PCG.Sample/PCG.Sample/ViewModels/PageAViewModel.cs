using Prism.Navigation;
using Prism.Generated;
using System.Threading.Tasks;

namespace PCG.Sample.ViewModels
{
    public class PageAViewModel : ViewModelBase
    {
        public PageAViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        protected override Task NavigateToAsync()
        {
            return NavigationService.NavigateAsync($"/{PageConstants.MainPage}");
        }
    }
}
