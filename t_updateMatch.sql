CREATE OR ALTER FUNCTION f_getPlayerLevel (@player_id int)
returns int
AS BEGIN
        declare @level int,
                @victories int,
                @defeats int,
                @matches int

        select @matches = (select count(*) from Match where player_two = @player_id OR player_one = @player_id)
        select @victories = (select count(*) from Match where winner = @player_id)
        select @defeats = @matches - @victories
        select @level = (1 + ((@victories - @defeats) % (select top 1 limit from GLOBAL_CONFIGURATION order by config_id desc)))
        RETURN @level
end
GO
drop trigger updateMatch
create or alter trigger updateMatch on MATCH
instead of update
AS BEGIN
    BEGIN try
    set transaction isolation level READ COMMITTED
    declare @match_id int,
            @state_id int,
            @type_id int,
            @winner int,
            @state_description varchar(20)


        select @match_id = match_id from inserted
        select @state_id = state_id from inserted
        select @winner = winner from inserted
        select @type_id = type_id from inserted

        select @state_id = (select state_id from MATCH where match_id = @match_id)
        select @state_description = (select state from STATE where state_id = @state_id)
        if((select count (*) from MATCH where player_one = @winner OR player_two = @winner) = 1)
        BEGIN
            if(@state_description = 'A decorrer')
            BEGIN
                update MATCH set state_id = (select state_id from STATE where state = 'Contabilizada'),
                                 winner = @winner
                                 where match_id = @match_id

                --start time secalhar devia ser, tempo decorrido mas afetava outras partes da implementação
                --assumi que caso o jogador perca, os seus atributos nao sao alterados
                --o acrescimo no nivel devera ser uma valor fixo
                update REGISTEREDPLAYER set level = dbo.f_getPlayerLevel(@winner),
                                            score += (select score from TYPE where type_id = @type_id),
                                            life_points += 1,
                                            strength_points += 1,
                                            speed_points += 1
                                            where player_id = @winner
            END
            else
            BEGIN
                raiserror ('Incorrect state for the match when trying to update the match != (A decorrer)', 16, 1)
            END
        END
        else
        BEGIN
            raiserror ('Player to update isn t in this match', 16, 1)
        END

    END try
    begin CATCH
        ROLLBACK
        print error_message()
        raiserror ('Failed to update the match', 16, 1)
    end CATCH
END
go

--update MATCH set winner = 3 where match_id = 4
