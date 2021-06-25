CREATE OR ALTER PROCEDURE createPlayerWithOptions @player_id int, @login_id int, @life_points int, @strength_points int, @speed_points int, @item_id int, @clan_id int
AS

INSERT INTO REGISTEREDPLAYER (player_id, login_id, life_points, strength_points, speed_points) VALUES(@player_id, @login_id, @life_points, @strength_points, @speed_points);

IF (@item_id IS NOT NULL)
	BEGIN TRY 
		UPDATE ITEM
		SET player_id = @player_id
		WHERE item_id = @item_id;
	END TRY
	BEGIN CATCH
	END CATCH

IF (@clan_id IS NOT NULL)
	BEGIN TRY 
		UPDATE REGISTEREDPLAYER
		SET clan = @clan_id
		WHERE player_id = @player_id
	END TRY
	BEGIN CATCH
	END CATCH


