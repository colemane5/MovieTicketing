CREATE PROCEDURE LogoutProcedure
	@UserEmail NVARCHAR(128)
AS
BEGIN
	UPDATE MovieDatabase.MovieDB.[User]
	SET IsLoggedIn = 0
	WHERE UserEmail = @UserEmail
END