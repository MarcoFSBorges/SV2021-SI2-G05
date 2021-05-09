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

    drop function if exists CheckMaxLevel
end try
begin catch
    rollback
    raiserror ('Error deleting tables!', 16, 1)
end catch