DROP TABLE IF EXISTS users;
DROP DATABASE IF EXISTS hack2gether;

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