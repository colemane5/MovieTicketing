CREATE OR ALTER PROCEDURE FindShowtimes
	@MovieID INT,
	@TheaterID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT M.MovieShowtimeID, M.StartOn, M.SeatsLeft, TP.SalePrice
	FROM MovieDB.MovieShowtime M
		INNER JOIN MovieDB.TicketPurchase TP ON TP.MovieShowtimeId = M.MovieShowtimeId
	WHERE M.MovieID = @MovieID
		AND M.TheaterID = @TheaterID
END