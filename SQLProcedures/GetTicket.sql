CREATE PROCEDURE GetTicket
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
		FROM MovieDatabase.MovieDB.TicketPurchase TP
		WHERE TP.UserID = @UserID 
			AND TP.MovieShowtimeID = @MovieShowtimeID
	)
	BEGIN
		INSERT INTO MovieDatabase.MovieDB.TicketPurchase(UserID, MovieShowtimeID, SalePrice)
		VALUES(@UserID, @MovieShowtimeID, @SalePrice)
		INSERT INTO MovieDatabase.MovieDB.Showtime(SeatsAvailable)
		VALUES(@SeatsLeft)
		SET @Result = N'Success'
	END
	ELSE
	BEGIN
		SET @Result = N'Failed'
	END
END