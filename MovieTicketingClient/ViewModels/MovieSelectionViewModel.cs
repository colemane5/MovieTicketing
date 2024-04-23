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
using SharedResources.SqlInterfaces;

namespace MovieTicketingClient.ViewModels
{
    public class Removeable<T>(T removeableElement) where T : IPerson
    {
        public T RemoveableElement { get; set; } = removeableElement;
        public int Id => RemoveableElement.Id;
        public string Name => RemoveableElement.Name;
        public DateTime DoB => RemoveableElement.DoB;
        public ICommand? RemoveCommand { get; set; }

        public void RegisterRemoveCommand(ICommand removeCommand)
        {
            RemoveCommand = removeCommand;
        }
    }
    public class MovieSelectionViewModel : RefreshableViewModel
    {
        private readonly SqlMovieRepository sqlMovieRepository = new();

        public string? UserEmail => _user?.Email;

        public List<Genre> Genres { get; }

        private Genre? _selectedGenre;
        public Genre? SelectedGenre
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

        public ObservableCollection<Removeable<Actor>> SelectedActors { get; } = [];

        public List<Director> Directors { get; }

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
        public ICommand LogoutCommand { get; }
        public ICommand SearchCommand { get; }

        public MovieSelectionViewModel() : base() 
        {
            LogoutCommand = Logout();
            AddActorCommand = new RelayCommand(() => { if (SelectedActor is Actor actor) AddRemoveableActor(actor); });
            SearchCommand = new RelayCommand(RefreshData);

            Actors = sqlMovieRepository.RetrieveActors();

            Directors = sqlMovieRepository.RetrieveDirectors();
            Director anyPlaceholderDirector = new Director(-1, "ANY", new DateTime());
            Directors = Directors.Prepend(anyPlaceholderDirector).ToList();
            SelectedDirector = anyPlaceholderDirector;

            Genres = sqlMovieRepository.RetrieveGenres();
            Genre anyPlaceholderGenre = new Genre(-1, "ANY");
            Genres = Genres.Prepend(anyPlaceholderGenre).ToList();
            SelectedGenre = anyPlaceholderGenre;
        }

        private void AddRemoveableActor(Actor actor)
        {
            if (!SelectedActors.Any(a => a.Id == actor.Id))
            {
                Removeable<Actor> removeable = new(actor);
                removeable.RegisterRemoveCommand(new RelayCommand(() => SelectedActors.Remove(removeable)));
                SelectedActors.Add(removeable); 
            }
        }

        public override void RefreshData()
        {
            string actorsList = string.Join(',', SelectedActors.Select(a => a.Name));
            if (SelectedGenre?.Name == "ANY") SelectedGenre = null;
            if (SelectedDirector?.Name == "ANY") SelectedDirector = null;
            FoundMovies = new(sqlMovieRepository.FilterMovies(
                null, 
                actorsList != string.Empty ? actorsList : null, 
                SelectedDirector?.Name,
                SelectedGenre?.Name
            ));
        }
    }
}
