CREATE PROCEDURE FindShowtimes
	@MovieID INT,
	@TheaterID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT M.StartOn, M.SeatsAvailable, TP.SalePrice
	FROM MovieDatabase.MovieDB.MovieShowtime M
		INNER JOIN MovieDatabase.MovieDB.TicketPurchase TP ON TP.MovieShowtimeId = M.MovieShowtimeId
	WHERE M.MovieID = @MovieID
		AND M.TheaterID = @TheaterID
END