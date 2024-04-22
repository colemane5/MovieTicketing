CREATE PROCEDURE CheckLoginStatus
	@UserEmail NVARCHAR(128),
	@UserName NVARCHAR(32) OUT,
	@UserID INT OUT,
	@IsAdmin BIT OUT
AS
BEGIN
	IF EXISTS(
		SELECT U.UserID
		FROM MovieDB.[User] U
		WHERE U.UserEmail = @UserEmail
			AND U.IsLoggedIn = 0
	)
	BEGIN
		SELECT @UserName = U.UserName, @UserID = U.UserID, @IsAdmin = U.IsAdmin
		FROM MovieDB.[User] U
		WHERE U.UserEmail = @UserEmail
	END
END