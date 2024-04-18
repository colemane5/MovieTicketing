CREATE PROCEDURE ManageTheater
	@Task NVARCHAR(16),
	@TheaterID INT = NULL,
	@TheaterName NVARCHAR(128),
	@TheaterAddress NVARCHAR(128),
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDatabase.MovieDB.Theater(TheaterName, TheaterAddress)
		VALUES(@TheaterName, @TheaterAddress)
		SET @Result = SCOPE_IDENTITY()
	END
	ELSE IF @Task = N'UPDATE'
		IF @TheaterID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END

		UPDATE MovieDatabase.MovieDB.Theater
		SET TheaterName = @TheaterName,
			TheaterAddress = @TheaterAddress
		WHERE TheaterID = @TheaterID
		SET @Result = 0
	END
	ELSE IF @Task = N'REMOVE'
	BEGIN
		IF @TheaterID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END

		DELETE FROM MovieDatabase.MovieDB.Theater
		WHERE TheaterID = @TheaterID
		SET @Result = 0
	END
	ELSE
	BEGIN
		SET @Result = -1
		RETURN
	END
END