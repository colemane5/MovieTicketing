CREATE OR ALTER PROCEDURE FindShowtimes
	@MovieID INT,
	@TheaterID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT DISTINCT M.MovieShowtimeID, M.StartOn, M.SeatsLeft, TP.SalePrice
	FROM MovieDB.MovieShowtime M
		LEFT JOIN MovieDB.TicketPurchase TP ON TP.MovieShowtimeId = M.MovieShowtimeId
	WHERE M.MovieID = @MovieID
		AND M.TheaterID = @TheaterID
END