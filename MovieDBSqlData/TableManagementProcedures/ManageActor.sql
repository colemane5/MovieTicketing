CREATE OR ALTER PROCEDURE ManageActor
	@Task NVARCHAR(16),
	@ActorID INT = NULL, --only used for update/remove
	@ActorName NVARCHAR(64),
	@ActorDoB DateTimeOffset,
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDB.Actor(ActorName, ActorDateOfBirth)
		VALUES(@ActorName, @ActorDoB)
		SET @Result = SCOPE_IDENTITY() --sets result to the id of the newly created actor
	END
	ELSE IF @Task = N'UPDATE'
	BEGIN
		IF @ActorID IS NULL
		BEGIN
			SET @Result = -1 --sets to -1 indicating a failure 
			RETURN
		END

		UPDATE MovieDB.Actor
		SET ActorName = @ActorName,
			ActorDateOfBirth = @ActorDoB
		WHERE ActorID = @ActorID
		SET @Result = 0 --sets to 0 indicating it successfuly completed the task
	END
	ELSE IF @Task = N'REMOVE'
	BEGIN
		IF @ActorID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END
		
		DELETE FROM MovieDB.Actor
		WHERE ActorID = @ActorID
		SET @Result = 0
	END
	ELSE
	BEGIN
		SET @Result = -1 --invalid action if it made it here, -1
		RETURN
	END
END
