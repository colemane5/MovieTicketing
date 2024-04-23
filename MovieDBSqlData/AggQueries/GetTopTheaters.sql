CREATE OR ALTER PROCEDURE GetTopTheaters
	@StartDate DATETIMEOFFSET,
	@EndDate DATETIMEOFFSET
AS
BEGIN

	WITH MonthlyTheaterSales AS(
		SELECT MONTH(MS.StartOn) AS SaleMonth,
		T.TheaterName,
		T.TheaterAddress,
		SUM(TP.SalePrice) AS TicketSales,
		ROW_NUMBER() OVER(PARTITION BY MONTH(MS.StartOn) ORDER BY SUM(TP.SalePrice) DESC) AS [Rank] --using row_number instead of rank to eliminate ties so we just get top 10, can change to rank if we want to include ties
		FROM MovieDB.MovieShowtime MS
		INNER JOIN MovieDB.Theater T ON MS.TheaterID = T.TheaterID
		LEFT JOIN MovieDB.TicketPurchase TP ON MS.MovieID = TP.MovieShowtimeID
			AND MS.StartOn BETWEEN @StartDate AND @EndDate
		GROUP BY MONTH(MS.StartOn), T.TheaterName, T.TheaterAddress
	)

	SELECT MT.SaleMonth AS [Month],
		MT.[Rank],
		MT.TheaterName,
		MT.TheaterAddress,
		MT.TicketSales
	FROM MonthlyTheaterSales MT
	WHERE MT.TicketSales IS NOT NULL
END
