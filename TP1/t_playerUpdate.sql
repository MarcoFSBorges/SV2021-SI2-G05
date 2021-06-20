create or alter trigger dbo.createUser on LOGIN
instead of insert
AS BEGIN
    set transaction isolation level READ COMMITTED
    declare @user_email varchar(50),
            @username varchar(50),
            @name varchar(50),
            @password varchar(100),
            @birthday date,
            @player_id int,
            @login_id int

    select @user_email = user_email from inserted
    if(@user_email IS NOT NULL)
    BEGIN
        select @username = username from inserted
        select @name = name from inserted
        select @password = password from inserted
        select @birthday = birthday from inserted
        insert into PLAYER (username) VALUES
        (@username);
        select @player_id = SCOPE_IDENTITY();
        insert into LOGIN (user_email, username, name, password, birthday) VALUES
        (@user_email, @username, @name, @password, @birthday);
        select @login_id = SCOPE_IDENTITY();
        insert into REGISTEREDPLAYER (player_id, login_id, life_points, strength_points, speed_points) VALUES
              (@player_id, @login_id, 1, 1, 1);
    END
    else
    BEGIN
        set @username = CONVERT(varchar(50), NEWID())
        insert into PLAYER (username) VALUES (@username)
        select @player_id = SCOPE_IDENTITY();
        INSERT INTO NONREGISTEREDPLAYER (player_id, name) VALUES (@player_id, @username);

    END
END
go

--insert into LOGIN (user_email, username, name, password, birthday) VALUES
--('test1@gmail.com', 'testerDummy1', 'Tester test1', 'fdsadsadasdas1', '1950-05-03');

--insert into LOGIN DEFAULT VALUES;

----------------

create or alter trigger dbo.updateUser on LOGIN
instead of update
as begin
    set transaction isolation level READ COMMITTED
       declare @user_email varchar(50),
            @username varchar(50),
            @name varchar(50),
            @password varchar(100),
            @birthday date
    select @user_email = user_email from inserted
    select @username = username from inserted
    select @name = name from inserted
    select @password = password from inserted
    select @birthday = birthday from inserted
    update LOGIN set user_email = @user_email,
                     name = @name,
                     password = @password,
                     birthday = @birthday
                 where username = @username;
end
go

--update LOGIN set birthday = '1908-05-21' where username = 'testerDummy'

--------------

create or alter trigger dbo.deleteUser on LOGIN
instead of delete
as begin
    set transaction isolation level READ COMMITTED
    declare @login_id int,
            @username varchar(50)

    select @username = username from deleted
    select @login_id = login_id from LOGIN where username = @username;
    update PLAYER set deleted = 1 where username = @username ;
end
go

--delete from LOGIN where username = 'testerDummy';