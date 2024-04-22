CREATE PROCEDURE RetrieveGenres
AS

SELECT G.GenreID, G.GenreName
FROM MovieDB.Genre G
GO