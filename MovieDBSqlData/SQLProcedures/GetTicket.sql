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

		SET @Result = 1;
	END
	ELSE
	BEGIN
		SET @Result = 0
	END
END
