CREATE OR ALTER PROCEDURE UpdateSeatsLeft
	@MovieShowtimeID INT
AS
BEGIN
	WITH CalcSeatsLeft AS (
		SELECT T.SeatsAvailable - COUNT(TP.MovieShowtimeID) AS NewSeatsLeft, @MovieShowtimeID AS MovieShowtimeID
		FROM MovieDB.Theater T
		LEFT JOIN MovieDB.MovieShowtime MS ON T.TheaterID = MS.TheaterID
		LEFT JOIN MovieDB.TicketPurchase TP ON MS.MovieShowtimeID = TP.MovieShowtimeID
		WHERE MS.MovieShowtimeID = @MovieShowtimeID
		GROUP BY T.SeatsAvailable
	)

	UPDATE MovieDB.MovieShowtime
	SET SeatsLeft = CS.NewSeatsLeft
	FROM MovieDB.MovieShowtime MS
	JOIN CalcSeatsLeft CS ON MS.MovieShowtimeID = CS.MovieShowtimeID
END



