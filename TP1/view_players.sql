CREATE OR ALTER FUNCTION dbo.Check_att (@att int)
RETURNS int
AS
BEGIN
    if(@att IS NULL)
    BEGIN
        return 0
    END
    RETURN @att;
END
GO

create or alter view dbo.players_view as
    select r.player_id, r.login_id, r.score, r.level, r.bank_balance,
           r.life_points + dbo.Check_att(i.bonus_life) as life_points,
           r.strength_points + dbo.Check_att(i.bonus_strength) as strength_points,
           r.speed_points + dbo.Check_att(i.bonus_speed) as speed_points,
           c.clan_name, i.item_id, i.name
    from ((REGISTEREDPLAYER r
        LEFT JOIN CLAN c ON r.clan = c.clan_id)
        LEFT JOIN (select player_id, item_id, name, bonus_life, bonus_strength, bonus_speed from ITEM where active = 1) i
            ON r.player_id = i.player_id)

create or alter trigger update_players_view on players_view
instead of update
as begin
    declare @item_id int
    declare @player_id int
    select @item_id = item_id from inserted
    select @player_id = player_id from inserted
    update ITEM set active = 1, player_id = @player_id where item_id = @item_id
end


select * from players_view;
