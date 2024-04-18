CREATE PROCEDURE GetTicket
	@UserID INT,
	@MovieShowtimeID INT,
	@SalePrice DECIMAL(10, 2),
	@Result NVARCHAR(10) OUT
AS
BEGIN
	SET NOCOUNT ON

	IF NOT EXISTS (
		SELECT TP.UserID
		FROM MovieDatabase.MovieDB.TicketPurchase TP
		WHERE TP.UserID = @UserID 
			AND TP.MovieShowtimeID = @MovieShowtimeID
	)
	BEGIN
		INSERT INTO MovieDatabase.MovieDB.TicketPurchase(UserID, MovieShowtimeID, SalePrice)
		VALUES(@UserID, @MovieShowtimeID, @SalePrice)
		SET @Result = N'Success'
	END
	ELSE
	BEGIN
		SET @Result = N'Failed'
	END
END