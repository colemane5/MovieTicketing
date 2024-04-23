CREATE OR ALTER PROCEDURE GetGenreRanks
	@StartTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY COUNT(T.UserID) DESC) AS [Rank],
	       G.GenreName,
	       COUNT(T.UserID) AS TicketsSold
	FROM MovieDB.Genre G
	INNER JOIN MovieDB.Movie M ON G.GenreID = M.GenreID
	INNER JOIN MovieDB.MovieShowtime MS ON M.MovieID = MS.MovieID
		AND MS.StartOn BETWEEN @StartTime AND @EndTime
	LEFT JOIN MovieDB.TicketPurchase T ON MS.MovieShowtimeID = T.MovieShowtimeID
	GROUP BY G.GenreName
	ORDER BY TicketsSold DESC
END