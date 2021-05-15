CREATE OR ALTER FUNCTION dbo.f_getPlayerGamesFromYear (@player_id int, @year int)
RETURNS @gamesTable TABLE(
            match_id int,
            state_id integer,
            type_id int,
            player_one INTEGER,
            player_two INTEGER,
            start_time DATE,
            winner INTEGER)

AS 
BEGIN

	IF(@player_id IS NOT NULL AND @year IS NOT NULL)
	BEGIN
		DECLARE	@match_id int,
					@state_id int,
					@type_id int,
					@player_one int,
					@player_two int,
					@start_time DATE,
					@winner int

		DECLARE get_games CURSOR FOR
			SELECT match_id, state_id, type_id, player_one, player_two, start_time, winner
			FROM MATCH;

		OPEN get_games
	
		FETCH NEXT FROM get_games INTO @match_id, @state_id, @type_id, @player_one, @player_two, @start_time, @winner;
		WHILE @@FETCH_STATUS = 0
		BEGIN
			IF(@player_one = @player_id OR @player_two = @player_id AND YEAR(@start_time) = @year)
			BEGIN
				INSERT INTO @gamesTable VALUES (@match_id, @state_id, @type_id, @player_one, @player_two, @start_time, @winner)
				FETCH NEXT FROM get_games INTO @match_id, @state_id, @type_id, @player_one, @player_two, @start_time, @winner;
			END
		END

		DEALLOCATE get_games;
	END
	RETURN
END

