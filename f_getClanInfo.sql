CREATE OR ALTER FUNCTION f_getClanInfo (@clanName varchar(20))
returns @clanTable table(
            clan_id int,
            clan_name VARCHAR(20),
            clan_score int)

AS BEGIN
        DECLARE  @clan_id int,
                 @clan_name VARCHAR(20),
                 @clan_score int

        if(ISNULL(@clanName, 'getCursor') = 'getCursor')
            BEGIN
                DECLARE get_NC CURSOR FOR
                    select clan_id, clan_name, clan_score
                    from CLAN;

                OPEN get_NC
                FETCH NEXT FROM get_NC INTO @clan_id, @clan_name, @clan_score;
                WHILE @@FETCH_STATUS = 0
                BEGIN
                    insert into @clanTable values (@clan_id, @clan_name, @clan_score)
                    FETCH NEXT FROM get_NC INTO @clan_id, @clan_name, @clan_score;
                end
                DEALLOCATE get_NC;
                RETURN
            END
        else
            BEGIN
                select @clan_id = clan_id, @clan_name = clan_name, @clan_score = clan_score  from CLAN where clan_name = @clanName
                insert into @clanTable values (@clan_id, @clan_name, @clan_score)
            END
        RETURN
end
GO

select * from dbo.f_getClanInfo (NULL);

select * from dbo.f_getClanInfo ('Moth Posse');
