CREATE OR ALTER PROCEDURE LoginUser
	@UserEmail NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(
		SELECT 1 
		FROM MovieDB.[User] U
			WHERE U.UserEmail = @UserEmail
			AND U.IsLoggedIn = 0
	)
	BEGIN
		
		UPDATE MovieDB.[User]
		SET IsLoggedIn = 1
		WHERE UserEmail = @UserEmail	
	END
END