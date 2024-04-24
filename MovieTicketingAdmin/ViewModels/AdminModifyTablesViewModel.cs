﻿using SharedResources.ViewModels;
using SharedResources.SqlInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using SharedResources.Models;
using System.Windows.Input;
using SharedResources.Commands;

namespace MovieTicketingAdmin.ViewModels
{
    public class AdminModifyTablesViewModel : RefreshableViewModel
    {
        //not sure which viewmodel this will be needed in so I put it in both.
        private readonly SqlManageMovieShowtime sqlShowtimeManager = new();
        private readonly SqlManageMovie sqlMovieManager = new();

        public List<Movie> MoviesList { get; } = [];
        public List<Theater> TheatersList { get; } = [];

        private Movie? _selectedMovie;
        public Movie? SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged(nameof(SelectedMovie));
                RefreshData();
            }
        }

        private ObservableCollection<Showtime> _currentShowtimes = [];
        public ObservableCollection<Showtime> CurrentShowtimes
        {
            get => _currentShowtimes;
            set
            {
                _currentShowtimes = value;
                OnPropertyChanged(nameof(CurrentShowtimes));
            }
        }

        private DateTime _newDate = DateTime.Now;
        public DateTime NewDate
        {
            get => _newDate;
            set
            {
                _newDate = value;
                OnPropertyChanged(nameof(NewDate));
            }
        }

        private string _newTime = "";
        public string NewTime
        {
            get => _newTime;
            set
            {
                _newTime = value;
                OnPropertyChanged(nameof(NewTime));
            }
        }

        private Theater? _selectedTheater;
        public Theater? SelectedTheater
        {
            get => _selectedTheater;
            set
            {
                _selectedTheater = value;
                OnPropertyChanged(nameof(SelectedTheater));
                RefreshData();
            }
        }

        public Showtime SelectedShowtime { get; set; }

        public ICommand AddNewShowtimeCommand { get; }
        public ICommand RemoveSelectedShowtimeCommand { get; }
        public ICommand BackCommand { get; }

        public AdminModifyTablesViewModel()
        {
            AddNewShowtimeCommand = new RelayCommand(AddNewShowtime);
            RemoveSelectedShowtimeCommand = new RelayCommand(RemoveSelectedShowtime);
            BackCommand = Navigation<AdminHomeViewModel>();

            MoviesList = sqlShowtimeManager.RetrieveMovies().ToList();
            TheatersList = sqlShowtimeManager.RetrieveTheaters().ToList();
        }

        private TimeSpan? ParseTime()
        {
            try
            {
                int[] times = NewTime.Replace("AM", "").Replace("PM", "").Split(':').Select(int.Parse).ToArray();
                int hours = times[0];
                int mins = times[1];
                if (NewTime.Contains("PM") && hours < 12 || hours == 12 && NewTime.Contains("AM"))
                    hours += 12;

                return new TimeSpan(hours, mins, 0);
            }
            catch (Exception) { return null; }
        }

        private void AddNewShowtime()
        {
            if (ParseTime() is not TimeSpan time) return;
            if (SelectedMovie is not Movie selectedMovie) return;
            if (SelectedTheater is not Theater selectedTheater) return;
            sqlShowtimeManager.ManageMovieShowtime("ADD", selectedMovie.Id, selectedTheater.Id, NewDate.Date + time, null);
            CurrentShowtimes.Add(new Showtime(-1, selectedMovie.Id, selectedTheater.Id, NewDate.Date + time, 10.00M, 100));
        }

        private void RemoveSelectedShowtime()
        {
            if (SelectedMovie is not Movie selectedMovie) return;
            if (SelectedTheater is not Theater selectedTheater) return;
            CurrentShowtimes.Remove(SelectedShowtime);
            sqlShowtimeManager.ManageMovieShowtime("REMOVE", selectedMovie.Id, selectedTheater.Id, SelectedShowtime.StartTime, null);
        }

        public override void RefreshData()
        {
            if (SelectedMovie is not Movie selectedMovie) return;
            if (SelectedTheater is not Theater selectedTheater) return;
            CurrentShowtimes = new(sqlShowtimeManager.FindShowtimes(selectedMovie.Id, selectedTheater.Id));
        }
    }
}
