CREATE PROCEDURE RetrieveActors
AS

SELECT A.ActorID, A.ActorName, A.ActorDateOfBirth
FROM MovieDB.Actor A
GO