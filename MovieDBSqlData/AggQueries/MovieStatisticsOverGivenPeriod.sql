CREATE OR ALTER PROCEDURE MovieStatistics
	@StartTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	SELECT M.MovieTitle,
	       COUNT(T.UserID) AS TicketSales,
		   COUNT(DISTINCT MS.MovieShowtimeID) AS TotalShowings,
	       IIF(COUNT(DISTINCT MS.MovieShowtimeID) > 0, ROUND(CAST(COUNT(DISTINCT T.UserID) AS FLOAT) / COUNT(DISTINCT MS.MovieShowtimeID), 0), 0) AS AvgTicketsPerShowing
	FROM MovieDB.Movie M
	LEFT JOIN MovieDB.MovieShowtime MS ON M.MovieID = MS.MovieID
		AND MS.StartOn BETWEEN @StartTime AND @EndTime
	LEFT JOIN MovieDB.TicketPurchase T ON T.MovieShowtimeID = MS.MovieShowtimeID
	GROUP BY M.MovieTitle
	ORDER BY TicketSales DESC, MovieTitle ASC
END