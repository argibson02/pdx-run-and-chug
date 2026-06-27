-- Seed script for MariaDB local development
-- Usage: mysql -u root -p runclub < mariaDbSeed.sql

CREATE DATABASE IF NOT EXISTS runclub;
USE runclub;

DROP TABLE IF EXISTS schedule;
DROP TABLE IF EXISTS locations;
DROP TABLE IF EXISTS links;

CREATE TABLE schedule (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Date DATE NOT NULL,
    Location VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    Notes VARCHAR(500) NULL
);

CREATE TABLE locations (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Address VARCHAR(255) NOT NULL,
    City VARCHAR(100) NOT NULL,
    State VARCHAR(10) NOT NULL,
    Zip VARCHAR(10) NOT NULL,
    Latitude DOUBLE NOT NULL,
    Longitude DOUBLE NOT NULL
);

CREATE TABLE links (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Url VARCHAR(500) NOT NULL
);

-- Schedule
INSERT INTO schedule (Date, Location, Status, Notes) VALUES
('2025-11-19', 'Living Haus', 'Confirmed', NULL),
('2025-11-26', 'Rogue', 'Confirmed', 'Thanksgiving Week'),
('2025-12-03', 'Level', 'Confirmed', NULL),
('2025-12-10', 'Baerlic', 'Confirmed', NULL),
('2025-12-17', 'Crux', 'Confirmed', NULL),
('2025-12-24', 'The Vern', 'Confirmed', 'Christmas'),
('2025-12-31', 'Baerlic', 'Confirmed', 'New Year''s Day'),
('2026-01-07', 'Bye & Bye', 'Confirmed', NULL),
('2026-01-14', 'Belmont Station', 'Confirmed', NULL),
('2026-01-21', 'Rogue', 'Confirmed', NULL),
('2026-01-28', 'Living Haus', 'Confirmed', NULL),
('2026-02-04', 'Paydirt', 'Confirmed', NULL),
('2026-02-11', 'Level', 'Confirmed', NULL),
('2026-02-18', 'Bye & Bye', 'Confirmed', NULL),
('2026-02-25', 'Crux', 'Confirmed', NULL),
('2026-03-04', 'Baerlic', 'Confirmed', NULL),
('2026-03-11', 'Rogue', 'Confirmed', NULL),
('2026-03-18', 'Imperial', 'Confirmed', NULL),
('2026-03-25', 'Belmont Station', 'Confirmed', NULL),
('2026-04-01', 'Living Haus', 'Confirmed', NULL),
('2026-04-08', 'Paydirt', 'Confirmed', NULL),
('2026-04-15', 'Level', 'Confirmed', NULL),
('2026-04-22', 'Baerlic', 'Confirmed', NULL),
('2026-04-29', 'Hawthorne Asylum', 'Confirmed', NULL),
('2026-05-06', 'BeerMongers', 'Confirmed', NULL),
('2026-05-13', 'Bye & Bye', 'Confirmed', NULL),
('2026-05-20', 'Rogue', 'Confirmed', NULL),
('2026-05-27', 'Imperial', 'Confirmed', NULL),
('2026-06-03', 'Baerlic', 'Confirmed', NULL),
('2026-06-10', 'Level', 'Confirmed', NULL),
('2026-06-17', 'Living Haus', 'Confirmed', NULL),
('2026-06-24', 'Hawthorne Asylum', 'Confirmed', NULL),
('2026-07-01', 'LOCAL Bye & Bye', 'Confirmed', 'test'),
('2026-07-08', 'Paydirt', 'In-Progress', NULL),
('2026-07-15', 'BeerMongers', 'Canceled', NULL),
('2026-07-22', 'Gorges', 'Confirmed', NULL),
('2026-07-29', 'Baerlic', 'Confirmed', NULL),
('2026-08-05', 'Rogue', 'Confirmed', NULL),
('2026-08-12', 'Living Haus', 'Confirmed', NULL),
('2026-08-19', 'Bye & Bye', 'Confirmed', 'Lita''s last day'),
('2026-08-26', 'Imperial', 'Confirmed', 'Michaela''s Bday'),
('2026-09-02', 'Hawthorne Asylum', 'Confirmed', NULL),
('2026-09-09', 'Level', 'Confirmed', NULL),
('2026-09-16', 'Paydirt', 'Confirmed', NULL),
('2026-09-23', 'Baerlic', 'Confirmed', NULL),
('2026-09-30', 'Living Haus', 'Confirmed', NULL),
('2026-10-07', 'BeerMongers', 'Confirmed', NULL),
('2026-10-14', 'Gorges', 'Confirmed', NULL),
('2026-10-21', 'Bye & Bye', 'Confirmed', NULL),
('2026-10-28', 'Gorges', 'Confirmed', NULL),
('2026-11-04', 'Belmont Station', 'Confirmed', NULL),
('2026-11-11', 'Imperial', 'Confirmed', NULL),
('2026-11-18', 'Imperial', 'Confirmed', NULL),
('2026-11-25', 'Rogue', 'Confirmed', NULL),
('2026-12-02', 'Baerlic', 'Confirmed', NULL),
('2026-12-09', 'Living Haus', 'Confirmed', 'Thanksgiving Week'),
('2026-12-16', 'Level', 'Confirmed', 'Cookie Swap'),
('2026-12-23', 'Space Room', 'Confirmed', 'Collab run at Tabor with Dirt Dorks Trail Club'),
('2026-12-30', 'Paydirt', 'Confirmed', NULL),
('2027-01-06', 'The Vern', 'Confirmed', 'Christmas Eve'),
('2027-01-13', 'BeerMongers', 'Confirmed', 'New Year''s Eve'),
('2027-01-20', 'Bye & Bye', 'Confirmed', NULL),
('2027-01-27', 'Belmont Station', 'Confirmed', NULL),
('2027-02-03', 'Bye & Bye', 'Confirmed', NULL),
('2027-02-10', 'Belmont Station', 'Confirmed', 'The day after Robin''s bday'),
('2027-02-17', 'Living Haus', 'Confirmed', NULL),
('2027-02-24', 'Baerlic', 'Confirmed', NULL),
('2027-03-03', 'Living Haus', 'Confirmed', NULL),
('2027-03-10', 'Baerlic', 'Confirmed', NULL),
('2026-02-04', 'Level', 'Confirmed', NULL),
('2026-02-11', 'Living Haus', 'Confirmed', NULL),
('2026-02-18', 'Level', 'Confirmed', NULL),
('2026-02-11', 'Paydirt', 'Confirmed', NULL),
('2026-02-18', 'Brooklyn Carreta', 'Confirmed', NULL),
('2026-02-25', 'BeerMongers', 'Confirmed', NULL);

-- Locations
INSERT INTO locations (Name, Address, City, State, Zip, Latitude, Longitude) VALUES
('Baerlic SE', '2239 SE 11th Ave', 'Portland', 'OR', '97214', 45.5067, -122.6551),
('BeerMongers', '2415 SE 11th Ave', 'Portland', 'OR', '97202', 45.5044, -122.6551),
('Belmont Station', '4500 SE Stark St', 'Portland', 'OR', '97215', 45.5191, -122.6154),
('Brooklyn Carreta', '4534 SE McLoughlin Blvd', 'Portland', 'OR', '97202', 45.4737, -122.6475),
('Bye & Bye', '1011 NE Alberta St', 'Portland', 'OR', '97211', 45.5593, -122.6552),
('Gorges', '2724 SE Ankeny St', 'Portland', 'OR', '97214', 45.5226, -122.6367),
('Hawthorne Asylum', '1080 SE Madison St', 'Portland', 'OR', '97214', 45.5116, -122.6561),
('Imperial', '3090 SE Division St', 'Portland', 'OR', '97202', 45.5054, -122.6335),
('Level 3', '1447 NE Sandy Blvd', 'Portland', 'OR', '97232', 45.5244, -122.652),
('Living Haus', '628 SE Belmont St', 'Portland', 'OR', '97214', 45.5164, -122.6562),
('Paydirt', '2724 NE Pacific St', 'Portland', 'OR', '97232', 45.5296, -122.6381),
('Space Room', '4800 SE Hawthorne Blvd', 'Portland', 'OR', '97215', 45.5121, -122.613),
('The Vern', '2622 SE Belmont St', 'Portland', 'OR', '97214', 45.5168, -122.6389);

-- Links
INSERT INTO links (Name, Url) VALUES
('Strava', 'https://strava.app.link/ynrdlIQui4b'),
('MeetUp', 'https://meetup.com/portland-fun-run?member_id=382819808'),
('Instagram', 'none');
