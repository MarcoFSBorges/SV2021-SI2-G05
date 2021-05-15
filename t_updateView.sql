create or alter trigger update_players_view on players_view
instead of update
as begin
    declare @item_id int
    declare @player_id int
    select @item_id = item_id from inserted
    select @player_id = player_id from inserted
    update ITEM set active = 1, player_id = @player_id where item_id = @item_id
end
