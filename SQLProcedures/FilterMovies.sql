CREATE PROCEDURE FilterMovies
	@MovieTitle NVARCHAR(128) = NULL,
	@ActorNames NVARCHAR(MAX) = NULL, --this needs to be a comma separated list of actors to filter by
	@Director NVARCHAR(64) = NULL, --i belive our data only has one director per movie so only allowing for queries with one director
	@GenreName NVARCHAR(128) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT M.MovieID, M.MovieTitle, M.ReleaseDate, M.MovieDescription
	FROM MovieDatabase.MovieDB.Movie M
	INNER JOIN MovieDatabase.MovieDB.Genre G ON M.GenreID = G.GenreID
	LEFT JOIN MovieDatabase.MovieDB.DirectorMovie DM ON M.MovieID = DM.MovieID
	LEFT JOIN MovieDatabase.MovieDB.Director D ON DM.DirectorID = D.DirectorID
	WHERE
		(@MovieTitle IS NULL OR M.MovieTitle LIKE '%' + @MovieTitle + '%') AND
		(@Director IS NULL OR D.DirectorName = @Director) AND
		(@GenreName IS NULL OR G.GenreName = @GenreName)AND
		(@ActorNames IS NULL OR EXISTS (
			SELECT MA.MovieID
			FROM MovieDatabase.MovieDB.ActorMovie MA
			WHERE MA.MovieID = M.MovieID
				AND MA.ActorID IN (
					SELECT ActorID
					FROM MovieDatabase.MovieDB.Actor A
					WHERE ActorName IN (SELECT value FROM STRING_SPLIT(@ActorNames, ','))
				)
			));
END