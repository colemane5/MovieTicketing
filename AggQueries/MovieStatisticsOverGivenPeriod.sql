CREATE PROCEDURE MovieStatisticsOverGivenPeriod
	@StartTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	SELECT M.MovieTitle,
	       COUNT(T.UserID) AS TicketSales,
		   COUNT(MS.MovieID) AS TotalShowings,
		   CAST(COUNT(T.UserID) AS FLOAT) / COUNT(MS.MovieID) AS AvgTicketsPerShowing
	FROM MovieDatabase.MovieDB.Movie M
	LEFT JOIN MovieDatabase.MovieDB.MovieShowtime MS ON M.MovieID = MS.MovieID
	LEFT JOIN MovieDatabase.MovieDB.TicketPurchase T ON T.MovieShowtimeID = MS.MovieID
	WHERE MS.StartOn BETWEEN @StartTime AND @EndTime
	GROUP BY M.MovieTitle
END