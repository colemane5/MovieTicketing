CREATE PROCEDURE ManageMovie
	@Task NVARCHAR(16),
	@MovieID INT = NULL,
	@MovieTitle NVARCHAR(128),
	@ReleaseDate DATE,
	@MovieDescription NVARCHAR(MAX),
	@GenreID INT,
	@Result INT OUT
AS
BEGIN
	IF @Task = N'ADD'
	BEGIN
		INSERT INTO MovieDatabase.MovieDB.Movie(MovieTitle, ReleaseDate, MovieDescription, GenreID)
		VALUES(@MovieTitle, @ReleaseDate, @MovieDescription, @GenreID)
		SET @Result = SCOPE_IDENTITY()
	END
	ELSE IF @Task = N'UPDATE'
	BEGIN
		IF @MovieID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END

		UPDATE MovieDatabase.MovieDB.Movie
		SET MovieTitle = @MovieTitle,
			ReleaseDate = @ReleaseDate,
			MovieDescription = @MovieDescription,
			GenreID = @GenreID
		WHERE MovieID = @MovieID
		SET @Result = 0
	END
	ELSE IF @Task = N'REMOVE'
	BEGIN
		IF @MovieID IS NULL
		BEGIN
			SET @Result = -1
			RETURN
		END

		DELETE FROM MovieDatabase.MovieDB.Movie
		WHERE MovieID = @MovieID
		SET @Result = 0
	END
	ELSE
	BEGIN
		SET @Result = -1
		RETURN
	END
END