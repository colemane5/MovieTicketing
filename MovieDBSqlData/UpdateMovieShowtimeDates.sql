
--Changes every rows month portion of their StartOn, 4 to 1, 5 to 2, etc.
UPDATE MovieDB.MovieShowtime
SET StartOn = 
	CASE
		WHEN DATEPART(MONTH, StartOn) = 4 THEN DATEADD(MONTH, -3, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 5 THEN DATEADD(MONTH, -4, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 6 THEN DATEADD(MONTH, -5, StartOn)
		WHEN DATEPART(MONTH, StartOn) = 7 THEN DATEADD(MONTH, -6, StartOn)
		ELSE StartOn
	END
WHERE DATEPART(MONTH, StartOn) IN (4, 5, 6, 7);


--Changes every rows day portion of their StartOn to a value between 1 and 28 ( just to avoid conflicts with february :p )
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 28, StartOn)

--Changes every rows time portion of their StartOn to a value between 17 and 21 if its not already | this will NEED to be ran until 0 rows affected, its using random values
--so it takes a few runs to hit all the correct times. you can see how many showtimes are left iwth the query at the bottom
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(HOUR, CAST(RAND() * (21 - 17 + 1) + 17 AS INT), StartOn)
WHERE DATEPART(HOUR, StartOn) NOT BETWEEN 17 AND 21

--Changes MovieShowtimeID 1 to StartOn January 1, 2024
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, 1 - DAY(StartOn), StartOn)
WHERE MovieShowtimeID = 1

--checking to see if there are any showtimes in the db with a time out of the range still
SELECT * FROM MovieDB.MovieShowtime
WHERE DATEPART(HOUR, StartOn) NOT BETWEEN 17 AND 21
