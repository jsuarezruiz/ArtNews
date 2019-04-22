using ArtNews.Models;
using ArtNews.Services;
using ArtNews.ViewModels.Base;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ArtNews.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        private Author _author;

        public Author Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChanged();
            }
        }

        public ICommand ArtDetailCommand => new Command<object>(ExecuteArtDetail);

        public override Task InitializeAsync(object navigationData)
        {
            LoadAuthorInfo();
            return base.InitializeAsync(navigationData);
        }

        private void LoadAuthorInfo()
        {
            Author = ArtService.Instance.GetAuthorInfo();
        }

        private void ExecuteArtDetail(object parameter)
        {
            NavigationService.Instance.NavigateToAsync<ArtDetailViewModel>(parameter, 1);
        }
    }
}