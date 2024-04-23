CREATE OR ALTER PROCEDURE ManageTheater
	@Task NVARCHAR(16),
	@TheaterID INT = NULL,
	@TheaterName NVARCHAR(128),
	@TheaterAddress NVARCHAR(128),
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDB.Theater(TheaterName, TheaterAddress)
		VALUES(@TheaterName, @TheaterAddress)
		SET @Result = SCOPE_IDENTITY()
	END
	ELSE IF @Task = N'UPDATE'
	BEGIN
		IF @TheaterID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END
		UPDATE MovieDB.Theater
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

		DELETE FROM MovieDB.Theater
		WHERE TheaterID = @TheaterID
		SET @Result = 0
	END
	ELSE
	BEGIN
		SET @Result = -1
		RETURN
	END
END