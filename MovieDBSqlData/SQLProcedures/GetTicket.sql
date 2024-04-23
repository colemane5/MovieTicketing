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

		UPDATE MovieDB.MovieShowtime
		SET SeatsLeft = (
			SELECT T.SeatsAvailable - COUNT(DISTINCT TP.MovieShowtimeID)
			FROM MovieDB.Theater T
			LEFT JOIN MovieDB.MovieShowtime M ON T.TheaterID = M.TheaterID
			LEFT JOIN MovieDB.TicketPurchase TP ON M.MovieShowtimeID = TP.MovieShowtimeID
			WHERE M.MovieShowtimeID = @MovieShowtimeID
		);

		SET @Result = 1;
	END
	ELSE
	BEGIN
		SET @Result = 0
	END
END
