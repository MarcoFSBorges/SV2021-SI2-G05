create or alter procedure dbo.p_giveItemToPlayer(@player_id int, @item_id)
as begin
    set nocount on
    set transaction isolation level serializable
    begin TRY
        begin transaction
            update ITEM set player_id = @player_id where item_id=@item_id
        COMMIT
    end TRY
    begin CATCH
        ROLLBACK
        print error_message()
        raiserror ('Failed to attribute item to player', 16, 1)
    end CATCH
    set nocount off
end