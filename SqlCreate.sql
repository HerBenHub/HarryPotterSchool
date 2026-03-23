CREATE TABLE spells (
    spell_id INT PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    `use` TEXT
);

CREATE TABLE characters (
    character_id INT PRIMARY KEY,
    full_name VARCHAR(255) NOT NULL,
    nickname VARCHAR(100),
    hogwarts_house VARCHAR(50),
    interpreted_by VARCHAR(255),
    image VARCHAR(500),
    birthdate DATE
);