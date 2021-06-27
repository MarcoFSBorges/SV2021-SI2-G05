CREATE OR ALTER PROCEDURE registerPlayerWithOptions @user_email varchar(50), @username varchar(50), @name varchar(50), @password varchar(100), @birthday date, @item_id int, @clan_name varchar(20)

AS

insert into LOGIN (user_email, username, name, password, birthday) VALUES
        (@user_email, @username, @name, @password, @birthday);

DECLARE @player_id int
SELECT @player_id = (SELECT player_id FROM PLAYER WHERE username = @username)

IF (@item_id IS NOT NULL)
	BEGIN TRY 
		UPDATE ITEM
		SET player_id = @player_id, active = 1
		WHERE item_id = @item_id;
	END TRY
	BEGIN CATCH
		SELECT 'Invalid Item ID.';
	END CATCH

IF (@clan_name IS NOT NULL)
	BEGIN TRY 
		DECLARE @clan_id int
		SELECT @clan_id = (SELECT clan_id FROM CLAN WHERE clan_name=@clan_name);
		IF (@clan_id IS NULL)
			THROW 51000, 'CLAN NOT FOUND', 10;
		UPDATE REGISTEREDPLAYER
		SET clan = @clan_id
		WHERE player_id = @player_id
	END TRY
	BEGIN CATCH
	SELECT 'Clan not found.';
	END CATCH
