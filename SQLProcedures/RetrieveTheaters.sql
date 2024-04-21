CREATE PROCEDURE RetrieveTheaters
	@MovieID INT
AS

SELECT T.TheaterID, T.TheaterName, T.TheaterAddress
FROM MovieDatabase.MovieDB.Theater T
WHERE EXISTS (
	SELECT *
	FROM MovieDatabase.MovieDB.Showtime S
	WHERE T.TheaterID = S.TheaterID
		AND S.MovieID = @MovieID
	)
GO