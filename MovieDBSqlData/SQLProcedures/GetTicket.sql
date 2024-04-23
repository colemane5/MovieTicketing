CREATE OR ALTER PROCEDURE GetTicket
	@UserID INT,
	@MovieShowtimeID INT,
	@SalePrice DECIMAL(10, 2),
	@SeatsLeft INT,
	@Result NVARCHAR(10) OUT
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
		SET SeatsAvailable = @SeatsLeft
		WHERE MovieShowtimeID = @MovieShowtimeID
		SET @Result = N'Success'
	END
	ELSE
	BEGIN
		SET @Result = N'Failed'
	END
END