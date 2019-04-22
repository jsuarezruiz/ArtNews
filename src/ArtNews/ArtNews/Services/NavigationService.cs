using ArtNews.ViewModels;
using ArtNews.ViewModels.Base;
using ArtNews.Views;
using Plugin.SharedTransitions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArtNews.Services
{
    public class NavigationService
    {
        private static NavigationService _instance;

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NavigationService();
                return _instance;
            }
        }

        protected readonly Dictionary<Type, Type> mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService()
        {
            mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            await NavigateToAsync<AuthorViewModel>();
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), null, null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), parameter, null);

        public Task NavigateToAsync<TViewModel>(object parameter, int tagGroup) where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), parameter, tagGroup);

        public Task NavigateToAsync(Type viewModelType) => InternalNavigateToAsync(viewModelType, null, null);

        public Task NavigateToAsync(Type viewModelType, object parameter) => InternalNavigateToAsync(viewModelType, parameter, null);

        public Task NavigateToAsync(Type viewModelType, object parameter, int tagGroup) => InternalNavigateToAsync(viewModelType, parameter, tagGroup);

        public async Task NavigateBackAsync()
        {
            await CurrentApplication.MainPage.Navigation.PopAsync();
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter, int? tagGroup)
        {
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is AuthorView)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    if(tagGroup != null)
                        SharedTransitionNavigationPage.SetSelectedTagGroup(page, tagGroup.Value);
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        void CreatePageViewModelMappings()
        {
            mappings.Add(typeof(ArtDetailViewModel), typeof(ArtDetailView));
            mappings.Add(typeof(AuthorViewModel), typeof(AuthorView));
        }
    }
}