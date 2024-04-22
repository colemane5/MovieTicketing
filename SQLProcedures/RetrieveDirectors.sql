CREATE PROCEDURE RetrieveDirectors
AS

SELECT D.DirectorID, D.DirectorName, D.DirectorDateOfBirth
FROM MovieDB.Director D
GO