CREATE OR ALTER PROCEDURE MovieStatistics
	@StartTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	SELECT M.MovieTitle,
	       COUNT(T.UserID) AS TicketSales,
		   COUNT(MS.MovieID) AS TotalShowings,
	       IIF(COUNT(MS.MovieID) > 0, CAST(COUNT(T.UserID) AS FLOAT) / COUNT(MS.MovieID), 0.00) AS AvgTicketsPerShowing
	FROM MovieDB.Movie M
	LEFT JOIN MovieDB.MovieShowtime MS ON M.MovieID = MS.MovieID
		AND MS.StartOn BETWEEN @StartTime AND @EndTime
	LEFT JOIN MovieDB.TicketPurchase T ON T.MovieShowtimeID = MS.MovieShowtimeID
	GROUP BY M.MovieTitle
	ORDER BY TicketSales DESC, MovieTitle ASC
END