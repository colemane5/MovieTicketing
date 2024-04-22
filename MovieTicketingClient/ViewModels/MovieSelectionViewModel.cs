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

        public ObservableCollection<Removeable<Actor>> SelectedActors { get; } = [];

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

        public ObservableCollection<Removeable<Director>> SelectedDirectors { get; } = [];

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
        public ICommand SearchCommand { get; }

        public MovieSelectionViewModel() : base() 
        {
            LogoutCommand = Logout();
            AddActorCommand = new RelayCommand(() => { if (SelectedActor is Actor actor) AddRemoveableActor(actor); });
            AddDirectorCommand = new RelayCommand(() => { if (SelectedDirector is Director director) AddRemoveableDirector(director); });
            SearchCommand = new RelayCommand(RefreshData);

            Actors =
            [
                new Actor(0, "Timothee Chalamet", DateTime.Now),
                new Actor(1, "Tim Chalam", DateTime.Now),
                new Actor(2, "Tom Chan", DateTime.Now)
            ];

            Directors =
            [
                new Director(0, "Timothee Chalamet", DateTime.Now),
                new Director(1, "Tim Chalam", DateTime.Now),
                new Director(2, "Tom Chan", DateTime.Now)
            ];
            Genres =
            [
                new Genre(0, "Action"),
                new Genre(1, "Comedy"),
                new Genre(2, "Drama")
            ];
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
        private void AddRemoveableDirector(Director director)
        {
            if (!SelectedDirectors.Any(d => d.Id == director.Id))
            {
                Removeable<Director> removeable = new(director);
                removeable.RegisterRemoveCommand(new RelayCommand(() => SelectedDirectors.Remove(removeable)));
                SelectedDirectors.Add(removeable); 
            }
        }

        public override void RefreshData()
        {
            throw new NotImplementedException();
        }
    }
}
