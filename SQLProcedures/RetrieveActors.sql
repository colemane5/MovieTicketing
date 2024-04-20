CREATE PROCEDURE RetrieveActors
AS

SELECT A.ActorName, A.ActorDateOfBirth
FROM MovieDatabase.MovieDB.Actor A
GO