CREATE OR ALTER PROCEDURE LogoutProcedure
	@UserEmail NVARCHAR(128)
AS
BEGIN
	UPDATE MovieDB.[User]
	SET IsLoggedIn = 0
	WHERE UserEmail = @UserEmail
END