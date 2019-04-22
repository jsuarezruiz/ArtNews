using ArtNews.ViewModels;
using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace ArtNews.Views
{
    public partial class ArtDetailView : ContentPage
    {
        public ArtDetailView(object parameter)
        {
            InitializeComponent();

            if(Device.RuntimePlatform == "Android")
                NavigationPage.SetHasNavigationBar(this, false);

            BindingContext = new ArtDetailViewModel(parameter);

            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }
    }
}