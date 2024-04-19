CREATE PROCEDURE GetSalesPerHourOfTheDay
	@StarTime DATETIMEOFFSET,
	@EndTime DATETIMEOFFSET
AS
BEGIN
	WITH HourlyStatistics AS (
		SELECT DATEPART(HOUR, MS.StartOn) AS HourOfDay,
			COUNT(DISTINCT M.MovieID) AS UniqueMovies,
			COUNT(DISTINCT MS.TheaterID) AS UniqueTheaters,
			SUM(T.UserID) AS TicketSales
		FROM MovieDatabase.MovieDB.MovieShowtime MS
		INNER JOIN MovieDatabase.MovieDB.TicketPurchase T ON MS.MovieID = T.MovieShowtimeID
		INNER JOIN MovieDatabase.MovieDB.Movie M ON MS.MovieID = M.MovieID
		WHERE MS.StartOn BETWEEN @StarTime AND @EndTime
		GROUP BY DATEPART(HOUR, MS.StartOn)
	)

	SELECT H.HourOfDay, H.UniqueMovies, H.UniqueTheaters, H.TicketSales
	FROM HourlyStatistics H
	ORDER BY HourOfDay
END