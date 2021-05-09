

create or alter function dbo.CheckMaxLevel(@level INTEGER, @life_points INTEGER, @strength_points INTEGER)
returns int
as begin
    declare @max_level int
    set @max_level = (select top 1 max_level from GLOBAL_CONFIGURATION order by config_id desc)

    if(@level <= @max_level and @life_points <= @max_level and @strength_points <= @max_level)
        return 0
    return 1
end
go

SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRY
    BEGIN TRANSACTION
        CREATE TABLE PLAYER(
            player_id int IDENTITY(0,1),  --starts at 0 and increments one for every entry
            name VARCHAR(30) NOT NULL,
            deleted BIT default 0,
            PRIMARY KEY (player_id)
        );

        CREATE TABLE LOGIN(
            login_id int IDENTITY(0,1),
            user_email varchar unique NOT NULL,
            username varchar(50) unique NOT NULL,
            name varchar(50) NOT NULL,
            password varchar(100) NOT NULL,
            birthday DATE NOT NULL,
            PRIMARY KEY (login_id),
            CONSTRAINT chk_email CHECK (user_email LIKE ('%_@__%.__%')),
        );

        CREATE TABLE GLOBAL_CONFIGURATION(
            config_id int IDENTITY(0,1),
            max_level int not null,
            clan_members int not null,
            limit int not null,
            CONSTRAINT chk_min_config CHECK (max_level > 0 AND clan_members > 0 AND limit > 0)
        );

         CREATE TABLE CLAN(
            clan_id int IDENTITY(0,1),
            clan_name VARCHAR(20) NOT NULL,
            clan_score INTEGER,
            PRIMARY KEY (clan_id),
        );

        CREATE TABLE REGISTEREDPLAYER(
            player_id int IDENTITY(0,1),
            login_id INTEGER,
            score INTEGER NOT NULL,
            level INTEGER NOT NULL,
            bank_balance INTEGER NOT NULL,
            life_points INTEGER NOT NULL,
            strength_points INTEGER NOT NULL,
            speed_points INTEGER NOT NULL,
            clan int NOT NULL,
            PRIMARY KEY (player_id),
            FOREIGN KEY (player_id) REFERENCES PLAYER(player_id),
            FOREIGN KEY (clan) REFERENCES CLAN(clan_id),
            FOREIGN KEY (login_id) REFERENCES LOGIN(login_id),
            CONSTRAINT chk_max_level CHECK (dbo.CheckMaxLevel(level, life_points, strength_points) = 0), --usa cache
            CONSTRAINT chk_balance_and_score CHECK (bank_balance >= 0 AND score >= 0)
        );

        CREATE TABLE NONREGISTEREDPLAYER(
            player_id INTEGER,
            name VARCHAR(30) NOT NULL,
            PRIMARY KEY (player_id),
            FOREIGN KEY (player_id) REFERENCES PLAYER(player_id)
        );

        CREATE TABLE STATE(
            state_id integer primary key,
            state varchar not null
        );

        CREATE TABLE TYPE(
            type_id int IDENTITY(0,1),
            min_life_points INTEGER NOT NULL,
            min_strength_points INTEGER NOT NULL,
            min_speed_points INTEGER NOT NULL,
            CONSTRAINT chk_min_type CHECK (min_life_points >= 0 AND min_strength_points >= 0 AND min_speed_points >= 0)
        );

        CREATE TABLE MATCH(
            match_id int IDENTITY(0,1),
            state_id integer,
            type_id int,
            player_one INTEGER,
            player_two INTEGER,
            start_time DATE default CURRENT_TIMESTAMP,
            winner INTEGER,
            PRIMARY KEY (match_id),
            FOREIGN KEY (player_one) REFERENCES PLAYER(player_id),
            FOREIGN KEY (player_two) REFERENCES PLAYER(player_id),
            FOREIGN KEY (winner) REFERENCES PLAYER(player_id)
        );



        CREATE TABLE ITEM(
              item_id int IDENTITY(0,1),
              player_id int default null,  --pode existir item sem estar referênciado a um player, para dar opções por exemplo em menus em caixas, etc
              name varchar(100) NOT NULL,
              bonus_life decimal(2, 2) default 0,
              bonus_strength decimal(2, 2) default 0,
              bonus_speed decimal(2, 2) default 0,
              PRIMARY KEY(item_id),
              CONSTRAINT check_minimum CHECK (bonus_life >= 0 AND bonus_strength >= 0 AND bonus_speed >= 0),
              FOREIGN KEY (player_id) REFERENCES PLAYER(player_id)
        );

        CREATE TABLE ITEM_PRICE(
            item_id integer check (item_id >= 0),
            price decimal(20, 3) check (price >= 0),
            --discount decimal(18, 3) not null default 0 check (discount >= 0 and discount <= 1),  --enunciado não referem discutir em grupo
            FOREIGN KEY (item_id) REFERENCES ITEM(item_id)
        );

    COMMIT
END TRY
BEGIN CATCH
    ROLLBACK
    RAISERROR ('Error at tables creation', 16, 1)
END CATCH