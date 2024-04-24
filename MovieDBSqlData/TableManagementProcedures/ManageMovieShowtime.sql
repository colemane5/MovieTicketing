CREATE OR ALTER PROCEDURE ManageMovieShowtimes
	@Task NVARCHAR(16), --this will be either "ADD", "UPDATE", or "REMOVE"
	@MovieID INT,
	@TheaterID INT,
	@StartOn DATETIMEOFFSET,
	@NewStartOn DATETIMEOFFSET = NULL, --only needed if updating, otherwise it will just remain null
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDB.MovieShowtime(MovieID, TheaterID, StartOn)
		VALUES(@MovieID, @TheaterID, @StartOn)
		SET @Result = SCOPE_IDENTITY() --sets result equal to the id of the newly created MovieShowtime
	END
	ELSE IF @Task = N'UPDATE'
	BEGIN
		UPDATE MovieDB.MovieShowtime
		SET StartOn = @NewStartOn
		WHERE MovieID = @MovieID
			AND TheaterID = @TheaterID
			AND StartOn = @StartOn
	END
	ELSE IF @Task = N'REMOVE'
	BEGIN
		DELETE FROM MovieDB.TicketPurchase 
		WHERE MovieShowtimeID IN(
			SELECT MS.MovieShowtimeID 
			FROM MovieDB.MovieShowtime MS
			WHERE MS.MovieID = @MovieID
				AND MS.TheaterID = @TheaterID
				AND MS.StartOn = @StartOn
		);


		DELETE FROM MovieDB.MovieShowtime
		WHERE MovieID = @MovieID
			AND TheaterID = @TheaterID
			AND StartOn = @StartOn
	END
	ELSE
	BEGIN
		SET @Result = -1; --invalid
		RETURN
	END

	
END
