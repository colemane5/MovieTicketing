IF SCHEMA_ID(N'MovieDB') IS NULL
	EXEC (N'CREATE SCHEMA MovieDB')
GO

DROP TABLE IF EXISTS MovieDB.ActorMovie
DROP TABLE IF EXISTS MovieDB.DirectorMovie
DROP TABLE IF EXISTS MovieDB.Actor
DROP TABLE IF EXISTS MovieDB.Director
DROP TABLE IF EXISTS MovieDB.TicketPurchase
DROP TABLE IF EXISTS MovieDB.[User]
DROP TABLE IF EXISTS MovieDB.MovieShowtime
DROP TABLE IF EXISTS MovieDB.Theater
DROP TABLE IF EXISTS MovieDB.Movie
DROP TABLE IF EXISTS MovieDB.Genre

CREATE TABLE MovieDB.Actor(
	ActorID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	ActorName NVARCHAR(64) NOT NULL,
	ActorDateOfBirth DATE,

	UNIQUE(ActorName, ActorDateOfBirth)
);

CREATE TABLE MovieDB.Director(
	DirectorID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	DirectorName NVARCHAR(64) NOT NULL,
	DirectorDateOfBirth DATE

	UNIQUE(DirectorName, DirectorDateOfBirth)
);

CREATE TABLE MovieDB.Genre(
	GenreID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	GenreName NVARCHAR(64) NOT NULL UNIQUE
);

CREATE TABLE MovieDB.Movie(
	MovieID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	GenreID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Genre(GenreID),
	MovieTitle NVARCHAR(128) NOT NULL,
	ReleaseDate DATETIMEOFFSET,
	[Description] NVARCHAR(MAX),

	UNIQUE(MovieTitle, ReleaseDate)
);

CREATE TABLE MovieDB.Theater(
	TheaterID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	TheaterName NVARCHAR(128) NOT NULL,
	TheaterAddress NVARCHAR(128),

	UNIQUE(TheaterName, TheaterAddress)
);

CREATE TABLE MovieDB.ActorMovie(
	MovieID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Movie(MovieID),
	ActorID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Actor(ActorID),

	UNIQUE(MovieID, ActorID),

	CONSTRAINT PKActorMovie PRIMARY KEY(MovieID, ActorID)
);

CREATE TABLE MovieDB.DirectorMovie(
	MovieID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Movie(MovieID),
	DirectorID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Director(DirectorID),

	UNIQUE(MovieID, DirectorID),
	CONSTRAINT PKDirectorMovie PRIMARY KEY(MovieID, DirectorID)
);

CREATE TABLE MovieDB.MovieShowtime(
	MovieShowtimeID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	MovieID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Movie(MovieID),
	TheaterID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.Theater(TheaterID),
	StartOn DATETIME NOT NULL,
	SeatsAvailable INT NOT NULL DEFAULT(38),

	UNIQUE(MovieID, TheaterID, StartOn)
);

CREATE TABLE MovieDB.[User](
	UserID INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	UserName NVARCHAR(32) NOT NULL,
	UserEmail NVARCHAR(128) NOT NULL UNIQUE,
	IsLoggedIn BIT DEFAULT(0),
	IsAdmin BIT DEFAULT(0)
);

CREATE TABLE MovieDB.TicketPurchase(
	UserID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.[User](UserID),
	MovieShowtimeID INT NOT NULL FOREIGN KEY
		REFERENCES MovieDB.MovieShowtime(MovieShowtimeID),
	SalePrice INT NOT NULL DEFAULT(10.97)
);