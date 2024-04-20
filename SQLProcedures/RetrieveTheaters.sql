CREATE PROCEDURE RetrieveTheaters
AS

SELECT T.TheaterID, T.TheaterName, T.TheaterAddress
FROM MovieDatabase.MovieDB.Theater T
GO