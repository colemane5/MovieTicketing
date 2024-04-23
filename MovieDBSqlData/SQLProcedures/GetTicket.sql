CREATE OR ALTER PROCEDURE GetTicket
	@UserID INT,
	@MovieShowtimeID INT,
	@SalePrice DECIMAL(10, 2),
	@Result BIT OUT
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
		SELECT TP.UserID
		FROM MovieDB.TicketPurchase TP
		WHERE TP.UserID = @UserID 
			AND TP.MovieShowtimeID = @MovieShowtimeID
	)
	BEGIN
		INSERT INTO MovieDB.TicketPurchase(UserID, MovieShowtimeID, SalePrice)
		VALUES(@UserID, @MovieShowtimeID, @SalePrice)

		DECLARE @Test INT

		WITH CalcSeatsLeft AS (
			SELECT T.SeatsAvailable - COUNT(TP.MovieShowtimeID) AS NewSeatsLeft
			FROM MovieDB.Theater T
			LEFT JOIN MovieDB.MovieShowtime MS ON T.TheaterID = MS.TheaterID
			LEFT JOIN MovieDB.TicketPurchase TP ON MS.MovieShowtimeID = TP.MovieShowtimeID
			WHERE MS.MovieShowtimeID = @MovieShowtimeID
			GROUP BY T.SeatsAvailable
		)

		UPDATE MovieDB.MovieShowtime
		SET SeatsLeft = (SELECT NewSeatsLeft FROM CalcSeatsLeft)

		SET @Result = 1;
	END
	ELSE
	BEGIN
		SET @Result = 0
	END
END
