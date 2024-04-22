CREATE PROCEDURE GetTopTheaters
AS
BEGIN
	DECLARE @StartDate DATETIME = DATEADD(YEAR, -1, GETDATE())
	DECLARE @EndDate DATETIME = GETDATE()

	WITH MonthlyTheaterSales AS(
		SELECT MONTH(MS.StartOn) AS SaleMonth,
		T.TheaterName,
		T.TheaterAddress,
		COUNT(TP.UserID) AS TicketSales,
		ROW_NUMBER() OVER(PARTITION BY MONTH(MS.StartOn) ORDER BY COUNT(TP.UserID) DESC) AS [Rank] --using row_number instead of rank to eliminate ties so we just get top 10, can change to rank if we want to include ties
		FROM MovieDB.MovieShowtime MS
		INNER JOIN MovieDB.Theater T ON MS.TheaterID = T.TheaterID
		LEFT JOIN MovieDB.TicketPurchase TP ON MS.MovieID = TP.MovieShowtimeID
			WHERE MS.StartOn BETWEEN @StartDate AND @EndDate
		GROUP BY MONTH(MS.StartOn), T.TheaterName, T.TheaterAddress
	)

	SELECT DATENAME(MONTH, DATEADD(MONTH, MT.SaleMonth -1, 0)) AS [Month],
		MT.[Rank],
		MT.TheaterName,
		MT.TheaterAddress,
		MT.TicketSales
	FROM MonthlyTheaterSales MT
	WHERE MT.Rank = 1 --will only return the highest ranked theater for each month
END
