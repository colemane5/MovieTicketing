CREATE PROCEDURE RetrieveMovies
AS

SELECT M.MovieID, M.MovieTitle, M.ReleaseDate, M.MovieDescription
FROM MovieDatabase.MovieDB.Movie M
GO