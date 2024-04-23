WITH CalcSeatsLeft AS (
	SELECT MS.MovieShowtimeID,
		T.SeatsAvailable - COUNT(TP.MovieShowtimeID) AS NewSeatsLeft
	FROM MovieDB.Theater T
	LEFT JOIN MovieDB.MovieShowtime MS ON T.TheaterID = MS.TheaterID
	LEFT JOIN MovieDB.TicketPurchase TP ON MS.MovieShowtimeID = TP.MovieShowtimeID
	GROUP BY MS.MovieShowtimeID, T.SeatsAvailable
)

UPDATE MovieDB.MovieShowtime
SET SeatsLeft = CS.NewSeatsLeft
FROM MovieDB.MovieShowtime MS
JOIN CalcSeatsLeft CS ON MS.MovieShowtimeID = CS.MovieShowtimeID



