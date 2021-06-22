IF OBJECT_ID (N'f_getPlayerGamesFromYear', N'IF') IS NOT NULL  
    DROP FUNCTION f_getPlayerGamesFromYear;  
GO  

CREATE FUNCTION f_getPlayerGamesFromYear (@player_id int, @year int)
RETURNS TABLE
            
AS 

RETURN( 
	SELECT match_id, state_id, type_id, player_one, player_two, start_time, winner
	FROM MATCH 
	WHERE player_one = @player_id OR player_two = @player_id AND YEAR(start_time) = @year
);