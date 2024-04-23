CREATE OR ALTER PROCEDURE RetrieveTheaters
	@MovieID INT = NULL --left null for cases where user needs all theaters
AS

SELECT T.TheaterID, T.TheaterName, T.TheaterAddress
FROM MovieDB.Theater T
WHERE @MovieID IS NULL OR EXISTS (
	SELECT *
	FROM MovieDB.MovieShowtime S
	WHERE T.TheaterID = S.TheaterID
		AND S.MovieID = @MovieID
	)
GO