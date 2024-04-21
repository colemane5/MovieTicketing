CREATE PROCEDURE LoginUser
	@UserEmail NVARCHAR(128),
	@IsLoggedIn BIT OUT,
	@UserID INT OUT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(
		SELECT 1 
		FROM MovieDatabase.MovieDB.[User] U
			WHERE U.UserEmail = @UserEmail
	)
	BEGIN
		
		UPDATE MovieDatabase.MovieDB.[User]
		SET IsLoggedIn = 1
		WHERE UserEmail = @UserEmail
		
		SET @IsLoggedIn = 1;

		SELECT @UserID = U.UserID
		FROM MovieDatabase.MovieDB.[User] U 
			WHERE U.UserEmail = @UserEmail;
	END
	ELSE
	BEGIN
		SET @IsLoggedIn = 0;
		SET @UserID = NULL;
	END
END