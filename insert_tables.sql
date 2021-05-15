DELETE FROM ITEM_PRICE
DELETE FROM ITEM
DELETE FROM MATCH
DELETE FROM TYPE
DELETE FROM STATE
DELETE FROM REGISTEREDPLAYER
DELETE FROM CLAN
DELETE FROM GLOBAL_CONFIGURATION
DELETE FROM LOGIN
DELETE FROM PLAYER

set transaction isolation level serializable
BEGIN TRY
	BEGIN TRANSACTION
    INSERT INTO PLAYER (username) VALUES
        ('MalhaVacas'),
        ('MestreDelas'),
        ('TheTherminator'),
        ('Muchacha'),
        ('AnitaPau'),
        ('VivaAoPipes'),
        ('MigustaTu'),
        ('GetReal');

    INSERT INTO LOGIN (user_email, username, name, password, birthday) VALUES
        ('MarcoBorges@gmail.com', 'MalhaVacas', 'Marco Borges', 'mgnevpa70h822Kx', '1996-05-03'),
        ('PedroPires@gmail.com', 'MestreDelas', 'Pedro Pires', 'rAXz5bR3DdqnVyI', '1999-10-08'),
        ('DanielAzevedo@hotmail.com', 'TheTherminator', 'Daniel Azevedo', 'VNl8iccweptzX2r', '1994-01-06'),
        ('LeiaCambezes@gmail.com', 'Muchacha', 'Leia Cambezes', 'lPl5z43VaB4JcTc', '1976-07-23'),
        ('AnitaAlpuim@hotmail.com', 'AnitaPau', 'Anita Alpuim', 'HORwhUKMtaZGyVX', '1983-05-24'),
        ('BogdanPaiva@gmail.com', 'VivaAoPipes', 'Bogdan Paiva', 'RGoHHicOSFzNi85', '2002-11-21'),
        ('AtílioEstrada@gmail.com', 'MigustaTu', 'Atílio Estrada', 'xS555GIcspRp08g', '1999-12-20'),
        ('LuenaNegrão@hotmail.com', 'GetReal', 'Luena Negrão', 'PAosMVGLFa6DLCF', '1984-06-18');

    INSERT INTO GLOBAL_CONFIGURATION (max_level, clan_members, limit) VALUES
        (50, 14, 20),
        (45, 12, 53),
        (54, 13, 52),
        (80, 13, 52);

     INSERT INTO CLAN (clan_name) VALUES
        ('Onyx Pygmy Squad'),
        ('Ivory Dragontooth'),
        ('Gold Knife Crew'),
        ('Moth Posse'),
        ('Flame Pincer Gang'),
        ('Cobalt Enigma Clan'),
        ('Gold Elephant Riders');

    INSERT INTO REGISTEREDPLAYER (player_id, login_id, life_points, strength_points, speed_points, clan) VALUES
       	(0, 0, 1, 1, 1, 1),
        (1, 1, 1, 1, 1, 2),
        (2, 2, 3, 3, 3, 6),
        (3, 3, 1, 1, 1, 4),
        (4, 4, 6, 6, 6, 3),
        (5, 5, 1, 1, 1, 2),
        (6, 6, 7, 7, 7, 1),
        (7, 7, 3, 3, 3, 2);

    INSERT INTO STATE (state_id, state) VALUES
        (1, 'Por iniciar'),
        (2, 'A decorrer'),
        (3, 'Terminada'),
        (4, 'Contabilizada');

    INSERT INTO TYPE (type_id, description, min_life_points, min_strength_points, min_speed_points) VALUES
        (1, 'DARKNIGHT MODE', 100, 50, 30),
        (2, 'FUCKING IMPOSSIBLE', 70, 36, 15),
        (3, 'KIM POSSIBLE', 10, 5, 10),
        (4, 'GOD OF WAR', 999, 999, 999);

    INSERT INTO MATCH (state_id, type_id, player_one) VALUES
        (1, 1, 1),
        (3, 2, 0),
        (4, 3, 2),
        (2, 3, 5),
        (2, 4, 3);

    INSERT INTO ITEM (player_id, name , bonus_life, bonus_strength, bonus_speed) VALUES
        (1, 'Diamond Sword', 1.50, 8.20, 2.00),
        (3, 'Magic Umbrella', 6.00, 2.40, 1.10),
        (4, 'Radiant Armor', 1.21, 8.00, 0.00),
        (2, 'Soccer Ball',10.00 ,0.00 ,60.00),
        (1, 'Invisibility Cap',0 ,0 ,8.40),
        (3, 'The sword of kings',5.50 ,5.50 ,0);

    INSERT INTO ITEM (name , bonus_life, bonus_strength, bonus_speed) VALUES
        ('Stick',5.50 ,5.50 ,0);

    INSERT INTO ITEM_PRICE (item_id, price) VALUES
        (0, 100000),
        (1, 500.69),
        (2, 9900000),
        (3, 9999999),
        (4, 54000),
        (5, 100);
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK
	raiserror('Failed tables filling',16,1)
END CATCH
