IF EXISTS (SELECT * FROM sys.databases WHERE name = 'hack2gether')
BEGIN
	DROP TABLE users;
	DROP DATABASE hack2gether;
END	

CREATE DATABASE hack2gether;
USE hack2gether;
CREATE TABLE users (
	id INT UNIQUE,
	username VARCHAR(255) NOT NULL UNIQUE,
	password VARCHAR(255) NOT NULL,
	email VARCHAR(255) NOT NULL UNIQUE,
	role VARCHAR(24) NOT NULL DEFAULT 'Student'
);
INSERT INTO users (id, username, password, email, role) VALUES
(000001, 'Bob', 'passbob', 'bob@otc.com', 'Student'),
(000002, 'Alice', 'passalice', 'alice@otc.com', 'Club Admin'),
(000003, 'Charlie', 'passcharlie', 'charlie@otc.com', 'Engagement Staff');

IF EXISTS (SELECT * FROM sys.databases WHERE name = 'hack2gether')
BEGIN
	DROP TABLE events;
END

CREATE TABLE events (
	id INT UNIQUE,
	name VARCHAR(255) NOT NULL,
	description TEXT,
	date DATE NOT NULL,
	location VARCHAR(255) NOT NULL,
	organizer_id INT NOT NULL,
	FOREIGN KEY (organizer_id) REFERENCES users(id)
);
INSERT INTO events (id, name, description, date, location, organizer_id) VALUES
(000001, 'Hackathon', 'A 24-hour coding event', '2024-10-01', 'OTC Main Hall', 000002),
(000002, 'Tech Talk', 'A talk on the latest tech trends', '2024-11-15', 'OTC Conference Room', 000003);