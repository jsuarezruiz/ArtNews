using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace ArtNews.Views
{
    public partial class AuthorView : ContentPage
    {
        public AuthorView()
        {
            InitializeComponent();

            SharedTransitionNavigationPage.SetBackgroundAnimation(this, BackgroundAnimation.Fade);
            SharedTransitionNavigationPage.SetSharedTransitionDuration(this, 500);
        }

        private async void OnHighlightTapped(object sender, System.EventArgs e)
        {
            // TODO: Fix shared element transition using the NavigationService.
            SharedTransitionNavigationPage.SetSelectedTagGroup(this, 1);     
            var context = (sender as View).BindingContext;
            await Navigation.PushAsync(new ArtDetailView(context));
        }
    }
}