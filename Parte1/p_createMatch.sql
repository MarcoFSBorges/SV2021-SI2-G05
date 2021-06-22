--if function return 0, the match is permitted
CREATE OR ALTER FUNCTION f_checkLevels (@player_one int, @player_two int, @type_id int)
returns int
AS BEGIN
    declare @life_points integer
    declare @strength_points integer
    declare @speed_points integer
    declare @match_life integer
    declare @match_strength integer
    declare @match_speed integer

     select @match_life = min_life_points, @match_strength = min_strength_points, @match_speed = min_speed_points
        from TYPE where type_id = @type_id

    select @life_points = life_points, @strength_points = strength_points, @speed_points = speed_points
        from REGISTEREDPLAYER where player_id = @player_one
    if(NOT(@life_points > @match_life AND @strength_points > @match_strength AND @speed_points > @match_speed))
        return 1

    select @life_points = life_points, @strength_points = strength_points, @speed_points = speed_points
        from REGISTEREDPLAYER where player_id = @player_two
    if(NOT(@life_points > @match_life AND @strength_points > @match_strength AND @speed_points > @match_speed))
        return 1

    return 0
END
GO


create or alter procedure dbo.p_createMatch(@player_one int, @player_two int, @description varchar(50))
as begin
    set nocount on
    set transaction isolation level READ COMMITTED
    begin try
        begin transaction
            declare @state_id integer
            declare @type_id int
            select @state_id = state_id from STATE where state = 'por iniciar'
            select @type_id = type_id from TYPE where description = @description

            --verificar se valores estão na tabela player
            if((select count(*) from PLAYER where player_id = @player_two) = 0
                   OR
               (select count(*) from PLAYER where player_id = @player_one) = 0)
                BEGIN
                     raiserror ('Players dont exist in the db', 16, 1)
                END
            else
            BEGIN
                if(dbo.f_checkLevels (@player_one, @player_two, @type_id) = 0)
                    BEGIN
                    INSERT INTO MATCH (state_id, type_id, player_one, player_two) VALUES
                 (@state_id, @type_id, @player_one, @player_two);
                    END
                else
                    BEGIN
                    raiserror ('Players dont have the required levels', 16, 1)
                    END
            END
        commit
    end try
    begin catch
        rollback
        print error_message()
        raiserror ('Match creation failed', 16, 1)
    end catch
    set nocount off
end


--EXEC dbo.p_createMatch 1, NULL, 'amigavel'
--EXEC dbo.p_createMatch 2, 3, 'contabilizada'
