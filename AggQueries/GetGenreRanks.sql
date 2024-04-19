CREATE PROCEDURE GetGenreRanks
	@StartTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY COUNT(T.UserID) DESC) AS [Rank],
	       G.GenreName,
	       COUNT(T.UserID) AS TicketsSold
	FROM MovieDatabase.MovieDB.Genre G
	INNER JOIN MovieDatabase.MovieDB.Movie M ON G.GenreID = M.GenreID
	INNER JOIN MovieDatabase.MovieDB.MovieShowtime MS ON M.MovieID = MS.MovieID
	LEFT JOIN MovieDatabase.MovieDB.TicketPurchase T ON MS.MovieID = T.MovieShowtimeID
	WHERE MS.StartOn BETWEEN @StartTime AND @EndTime
	GROUP BY G.GenreName
	ORDER BY TicketsSold DESC
END