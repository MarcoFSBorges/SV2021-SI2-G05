set transaction isolation level serializable
begin try
    drop table if exists ITEM_PRICE
    drop table if exists ITEM
    drop table if exists MATCH
    drop table if exists TYPE
    drop table if exists STATE
    drop table if exists NONREGISTEREDPLAYER
    drop table if exists REGISTEREDPLAYER
    drop table if exists CLAN
    drop table if exists GLOBAL_CONFIGURATION
    drop table if exists LOGIN
    drop table if exists PLAYER

    drop view if exists players
    drop view if exists players_view
    drop function if exists check_att
    drop function if exists CheckMaxLevel
    drop procedure if exists createPlayerWithOptions
    drop function if exists f_checkLevels
    drop function if exists f_getClanInfo
    drop procedure if exists p_createMatch
    drop procedure if exists p_giveItemToPlayer
    drop function if exists f_getPlayerLevel
    drop trigger if exists updateMatch

end try
begin catch
    rollback
    raiserror ('Error deleting tables!', 16, 1)
end catch