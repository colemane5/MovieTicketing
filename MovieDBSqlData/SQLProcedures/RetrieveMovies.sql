CREATE OR ALTER PROCEDURE RetrieveMovies
AS

SELECT M.MovieID, M.MovieTitle, M.ReleaseDate, M.[Description]
FROM MovieDB.Movie M
GO