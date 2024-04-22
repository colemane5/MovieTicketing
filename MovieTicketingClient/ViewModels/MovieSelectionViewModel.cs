using SharedResources.Commands;
using SharedResources.Models;
using SharedResources.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MovieTicketingClient.ViewModels
{
    public class MovieSelectionViewModel : RefreshableViewModel
    {
        public List<Genre> Genres { get; } = null!;

        private Genre _selectedGenre;
        public Genre SelectedGenre
        {
            get => _selectedGenre;
            set
            {
                _selectedGenre = value;
                OnPropertyChanged(nameof(SelectedGenre));
            }
        }

        public List<Actor> Actors { get; } = null!;

        private Actor? _selectedActor;
        public Actor? SelectedActor
        {
            get => _selectedActor;
            set
            {
                _selectedActor = value;
                OnPropertyChanged(nameof(SelectedActor));
            }
        }

        public ObservableCollection<Actor> SelectedActors { get; } = [];

        public List<Director> Directors { get; } = null!;

        private Director? _selectedDirector;
        public Director? SelectedDirector
        {
            get => _selectedDirector;
            set
            {
                _selectedDirector = value;
                OnPropertyChanged(nameof(SelectedDirector));
            }
        }

        public ObservableCollection<Director> SelectedDirectors { get; } = [];

        private ObservableCollection<Movie> _foundMovies = [];
        public ObservableCollection<Movie> FoundMovies
        {
            get => _foundMovies;
            set
            {
                _foundMovies = value;
                OnPropertyChanged(nameof(FoundMovies));
            }
        }

        public ICommand AddActorCommand { get; }
        public ICommand AddDirectorCommand { get; }
        public ICommand LogoutCommand { get; }

        public MovieSelectionViewModel() : base() 
        {
            LogoutCommand = Logout();
            AddActorCommand = new RelayCommand(() => {
                if (SelectedActor is Actor actor)
                    SelectedActors.Add(actor);
            });
            AddDirectorCommand = new RelayCommand(() => {
                if (SelectedDirector is Director director)
                    SelectedDirectors.Add(director);
            });
        }

        public override void RefreshData()
        {
            throw new NotImplementedException();
        }
    }
}
