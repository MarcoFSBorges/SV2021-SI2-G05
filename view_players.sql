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

use SI2_SemestreVerao

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


select * from players_view;
