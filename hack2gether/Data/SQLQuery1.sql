IF EXISTS (SELECT * FROM sys.databases WHERE name = 'hack2gether')
BEGIN
	DROP TABLE events;
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
	role VARCHAR(24) NOT NULL DEFAULT 'Student',
	Points INT NOT NULL DEFAULT 0
);
INSERT INTO users (id, username, password, email, role, Points) VALUES
(000001, 'Bob', 'passbob', 'bob@otc.com', 'Student', 0),
(000002, 'Alice', 'passalice', 'alice@otc.com', 'Club Admin', 0),
(000003, 'Charlie', 'passcharlie', 'charlie@otc.com', 'Engagement Staff', 0);
CREATE TABLE events (
	id INT UNIQUE,
	name VARCHAR(255) NOT NULL,
	description TEXT,
	date DATE NOT NULL,
	location VARCHAR(255) NOT NULL,
	admin_id INT NOT NULL,
	FOREIGN KEY (admin_id) REFERENCES users(id)
);
INSERT INTO events (id, name, description, date, location, admin_id) VALUES
(1, 'Tech2gether', 'A 24-hour coding event', '2024-10-01', 'OTC Main Hall', 000002),
(2, 'Tech2gether', 'A talk on the latest tech trends', '2024-11-15', 'OTC Conference Room', 000002);

-- set existing nulls to 0
UPDATE dbo.Users SET Points = 0 WHERE Points IS NULL;
-- add default constraint if not present
IF NOT EXISTS (
    SELECT 1 FROM sys.default_constraints dc
    JOIN sys.columns c ON dc.parent_object_id = c.object_id AND dc.parent_column_id = c.column_id
    WHERE OBJECT_NAME(dc.parent_object_id) = 'Users' AND c.name = 'Points'
)
BEGIN
    ALTER TABLE dbo.Users ADD CONSTRAINT DF_Users_Points DEFAULT 0 FOR Points;
END
-- make column NOT NULL
ALTER TABLE dbo.Users ALTER COLUMN Points INT NOT NULL;