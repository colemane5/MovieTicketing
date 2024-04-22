CREATE PROCEDURE CheckLoginStatus
	@UserEmail NVARCHAR(128)
AS
BEGIN
	IF EXISTS(
		SELECT U.UserID
		FROM MovieDB.[User] U
		WHERE U.UserEmail = @UserEmail
			AND U.IsLoggedIn = 0
	)
	BEGIN
		SELECT U.UserName, U.UserID, U.IsAdmin
		FROM MovieDB.[User] U
		WHERE U.UserEmail = @UserEmail
	END
END