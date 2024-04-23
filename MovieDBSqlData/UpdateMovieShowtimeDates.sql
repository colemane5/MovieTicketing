
--Changes every rows month portion of their StartOn, 4 to 1, 5 to 2, etc.
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEFROMPARTS(
	YEAR(StartOn), CASE WHEN DATEPART(MONTH, StartOn) = 4 THEN 1
						 WHEN DATEPART(MONTH, StartOn) = 5 THEN 2
						 WHEN DATEPART(MONTH, StartOn) = 6 THEN 3
						 WHEN DATEPART(MONTH, StartOn) = 7 THEN 4
					END,
	RAND()*(14-1)+1
)


--Changes every rows day portion of their StartOn to a value between 1 and 28 ( just to avoid conflicts with february :p )
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, ABS(CHECKSUM(NEWID())) % 28, StartOn)


--Changes every rows time portion of their StartOn to a value between 17 and 21
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(HOUR, 17 + ABS(CHECKSUM(NEWID())) % 5, StartOn)

--Changes MovieShowtimeID 1 to StartOn January 1, 2024
UPDATE MovieDB.MovieShowtime
SET StartOn = DATEADD(DAY, 1 - DAY(StartOn), StartOn)
WHERE MovieShowtimeID = 1

SELECT * FROM MovieDB.MovieShowtime