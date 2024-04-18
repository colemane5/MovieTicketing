CREATE PROCEDURE FindShowtimes
	@MovieID INT,
	@TheaterID INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT M.MovieID, M.StartOn
	FROM MovieDatabase.MovieDB.MovieShowtime M
	WHERE M.MovieID = @MovieID
		AND M.TheaterID = @TheaterID
END