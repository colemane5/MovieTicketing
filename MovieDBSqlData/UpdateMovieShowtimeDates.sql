
--Changes every rows month portion of their StartOn, 4 to 1, 5 to 2, etc.
UPDATE MovieDB.MovieShowtime
SET StartOn = 
	CASE
		WHEN DATEPART(MONTH, StartOn) = 4 THEN DATEADD(MONTH, 1, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 5 THEN DATEADD(MONTH, 2, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 6 THEN DATEADD(MONTH, 3, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 7 THEN DATEADD(MONTH, 4, StartOn)
		ELSE StartOn
	END
WHERE DATEPART(MONTH, StartOn) IN (4, 5, 6, 7);


--Changes every rows day portion of their StartOn to a value between 1 and 28 ( just to avoid conflicts with february :p )
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 28, StartOn)

--Changes every rows time portion of their StartOn to a value between 17 and 21 if its not already
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(HOUR, CAST(RAND() * (21 - 17 + 1) + 17 AS INT), StartOn)
WHERE DATEPART(HOUR, StartOn) NOT BETWEEN 17 AND 21

--Changes MovieShowtimeID 1 to StartOn January 1, 2024
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, 1 - DAY(StartOn), StartOn)
WHERE MovieShowtimeID = 1

--checking to see if the random values were random enough (didnt create duplicate keys)
SELECT * FROM MovieDB.MovieShowtime
WHERE DATEPART(HOUR, StartOn) NOT BETWEEN 17 AND 21