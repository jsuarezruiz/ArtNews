using ArtNews.Services;
using ArtNews.ViewModels;
using ArtNews.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArtNews
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BuildDependencies();
            InitNavigation();
        }

        void BuildDependencies()
        {
            Locator.Instance.Build();
        }

        Task InitNavigation()
        {
            return NavigationService.Instance.NavigateToAsync<AuthorViewModel>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
