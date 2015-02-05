
CREATE TABLE kagaribi_player (
	id varchar(100) collate utf8_unicode_ci default NULL,
	name varchar(10),
	PRIMARY KEY (id)
) ENGINE=InnoDB;


CREATE TABLE kagaribi_ranking (
	player_id varchar(100) collate utf8_unicode_ci default NULL,
	score int default NULL,
	time datetime default '0000-00-00 00:00:00',
	PRIMARY KEY (player_id, time),
	FOREIGN KEY (player_id)
		REFERENCES kagaribi_player (id)
) ENGINE=InnoDB;
