CREATE PROCEDURE ManageActor
	@Task NVARCHAR(16),
	@DirectorID INT = NULL, --only used for update/remove
	@DirectorName NVARCHAR(64),
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDatabase.MovieDB.Director(DirectorName)
		VALUES(@DirectorName)
		SET @Result = SCOPE_IDENTITY() --sets result to the id of the newly created actor
	END
	ELSE IF @Task = N'UPDATE'
	BEGIN
		IF @DirectorID IS NULL
		BEGIN
			SET @Result = -1 --sets to -1 indicating a failure 
			RETURN
		END

		UPDATE MovieDatabase.MovieDB.Director
		SET DirectorName = @DirectorName
		WHERE DirectorID = @DirectorID
		SET @Result = 0 --sets to 0 indicating it successfuly completed the task
	END
	ELSE IF @Task = N'REMOVE'
	BEGIN
		IF @DirectorID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END
		
		DELETE FROM MovieDatabase.MovieDB.Director
		WHERE DirectorID = @DirectorID
		SET @Result = 0
	END
	ELSE
	BEGIN
		SET @Result = -1 --invalid action if it made it here, -1
		RETURN
	END
END
