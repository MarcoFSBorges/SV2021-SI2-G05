create or alter procedure dbo.p_giveItemToPlayer(@player_id int, @item_id int)
as begin
    set nocount on
    set transaction isolation level serializable
    begin TRY
        begin transaction
            if ((select player_id from dbo.ITEM where item_id = @item_id) IS NULL)
				begin
                    update dbo.ITEM set active = 0 where active = 1;
					update dbo.ITEM set player_id = @player_id where item_id = @item_id
				end
            else
				begin
					raiserror ('Failed to attribute item to player', 16, 1)
				end
        COMMIT
    end TRY
    begin CATCH
        ROLLBACK
        print error_message()
        raiserror ('Failed to attribute item to player', 16, 1)
    end CATCH
    set nocount off
end