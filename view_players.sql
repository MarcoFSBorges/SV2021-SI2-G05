use SI2_SemestreVerao;
create or alter view players_view as
    select r.player_id, r.login_id, r.score, r.level, r.bank_balance,
           r.life_points + i.bonus_life as life_points,
           r.strength_points + i.bonus_strength as strength_points,
           r.speed_points + i.bonus_speed as speed_points,
           c.clan_name, i.item_id, i.name
    from ((REGISTEREDPLAYER r
        LEFT JOIN CLAN c ON r.clan = c.clan_id)
        INNER JOIN (select player_id, item_id, name, bonus_life, bonus_strength, bonus_speed from ITEM where active = 1) i
            ON r.player_id = i.player_id)
        UNION
            (select r.player_id, r.login_id, r.score, r.level, r.bank_balance,
               r.life_points, r.strength_points, r.speed_points,
               c.clan_name, i.item_id, i.name
            from (((REGISTEREDPLAYER r
                LEFT JOIN CLAN c ON r.clan = c.clan_id)
                LEFT JOIN ITEM i
                    ON r.player_id = i.player_id)
                 )where i.item_id IS NULL OR active = 0)

--pode não ter item
--




create or alter trigger update_players_view on players_view
instead of update
as begin
    from REGISTEREDPLAYER r
        LEFT JOIN
        /*
    update REGISTEREDPLAYER r
        set life_points = inserted.life_points from inserted,
            strength_points = inserted.strength_points from inserted,
            speed_points = inserted.speed_points from inserted
        where player_id = 0;
         */
end
go


select * from players_view;


--dados dos jogadores
--nome e identificador do item ativo
--caso o item exista retornar também valores de vida, força, e velocidade

/*
(i) Criar uma vista, que devolva os dados dos jogadores. A lista também deve apresentar o
nome e o identificador do item ativo para esse jogador, caso exista e nesse caso, os valores de
vida, força e velocidade intrínsecos devem ser somados aos do seu item ativo.
 */