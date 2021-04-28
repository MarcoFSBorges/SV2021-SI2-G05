CREATE TABLE PLAYER(
	player_id SERIAL,
	name VARCHAR(30) NOT NULL,
	deleted BIT NOT NULL,
	PRIMARY KEY (player_id)
);

CREATE TABLE REGISTEREDPLAYER(
	player_id INTEGER,
	name VARCHAR(30) NOT NULL,
	username VARCHAR(30) NOT NULL,
	password VARCHAR(30) NOT NULL,
	birthday DATE NOT NULL,
	score INTEGER NOT NULL,
	level INTEGER NOT NULL,
	bank_balance INTEGER NOT NULL,
	life_points INTEGER NOT NULL,
	strength_points INTEGER NOT NULL,
	speed_points INTEGER NOT NULL,
	clan INTEGER NOT NULL,
	PRIMARY KEY (player_id),
	FOREIGN KEY (player_id) REFERENCES PLAYER(player_id),
	FOREIGN KEY (clan) REFERENCES CLAN(clan_id)
);

CREATE TABLE NONREGISTEREDPLAYER(
	player_id INTEGER,
	name VARCHAR(30) NOT NULL,
	PRIMARY KEY (player_id),
	FOREIGN KEY (player_id) REFERENCES PLAYER(player_id)
);

CREATE TABLE MATCH(
	match_id SERIAL,
	player_one INTEGER,
	player_two INTEGER,
	type VARCHAR(10) ,
	CONSTRAINT chk_type CHECK (type IN ('Ranked', 'Unranked')),
	start_time DATE,
	winner INTEGER,
	state VARCHAR(10),
	CONSTRAINT chk_state CHECK (state IN ('Scheduled', 'Ongoing', 'Finished', 'Processed')),
	min_life_points INTEGER,
	min_strength_points INTEGER,
	min_speed_points INTEGER,
	PRIMARY KEY (match_id),
	FOREIGN KEY (player_one) REFERENCES PLAYER(player_id),
	FOREIGN KEY (player_two) REFERENCES PLAYER(player_id),
	FOREIGN KEY (winner) REFERENCES PLAYER(player_id)
);

CREATE TABLE CLAN(
	clan_id SERIAL,
	max_members INTEGER, 
	clan_name VARCHAR(30), 
	clan_score INTEGER,
	PRIMARY KEY (clan_id),
);
