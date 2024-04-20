CREATE PROCEDURE RetrieveDirectors
AS

SELECT D.DirectorName, D.DirectorDateOfBirth
FROM MovieDatabase.MovieDB.Director D
GO