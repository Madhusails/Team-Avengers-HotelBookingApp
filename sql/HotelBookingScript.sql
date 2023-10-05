-- creating database
create database HotelBookingDB
go

-- switching to database
use HotelBookingDB
go

-- room type table
create table RoomType (
	Room_id int primary key,
	Room_type nvarchar(50) not null
	)

-- room table
create table RoomInfo (
	RoomNumber int primary key identity(101, 1),
	RoomId int not null,
	isBooked bit,
	foreign key(RoomId) references RoomType(Room_id)
	);

insert into RoomType values(1, 'AC');
insert into RoomType values(2, 'Non-AC');
insert into RoomType values(3, 'AC Deluxe');
insert into RoomType values(4, 'Suit Room');

select * from RoomType
select * from RoomInfo

insert into RoomInfo(RoomId) values(1);
insert into RoomInfo(RoomId) values(2);
insert into RoomInfo(RoomId) values(3);
insert into RoomInfo(RoomId) values(3);
insert into RoomInfo(RoomId) values(1);
insert into RoomInfo(RoomId) values(2);


-- creating userinfo table
CREATE TABLE UserInfo (
    UserId int PRIMARY KEY Identity(1,1),
    Name VARCHAR(255) NOT NULL,
    Age INT,
    No_of_guests INT,
    No_of_rooms INT,
    Checkin DATE,
    Checkout DATE,
    Number_of_days INT
);


-- creating registeredusers table
CREATE TABLE RegisteredUsers(
Id int PRIMARY KEY Identity(1,1),
Email VARCHAR(100),
UserName VARCHAR(50),
Password VARCHAR(50)
);

select * from RegisteredUsers
truncate table registeredusers

select * from RoomInfo
select * from UserInfo
-- alter roomtype
CREATE TABLE RoomInfo (
    RoomNumber int primary key identity(101, 1),
    RoomId int not null,
    Checkin DateTime default GETDATE(),
    Checkout DateTime default GETDATE(),
    isBooked bit,
    foreign key(RoomId) references RoomType(Room_id)
);


drop table RoomInfo

insert into RoomInfo(RoomId,isBooked) values(1,0);
insert into RoomInfo(RoomId,isBooked) values(2,0);
insert into RoomInfo(RoomId,isBooked) values(3,0);
insert into RoomInfo(RoomId,isBooked) values(3,0);
insert into RoomInfo(RoomId,isBooked) values(1,0);
insert into RoomInfo(RoomId,isBooked) values(2,0);

-- stored procedures
CREATE PROCEDURE CreateUser
    @Email NVARCHAR(255),
    @UserName NVARCHAR(50),
    @Password NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO RegisteredUsers (Email, UserName, Password)
    VALUES (@Email, @UserName, @Password);

    SELECT CAST(SCOPE_IDENTITY() AS INT);
END;

-- testing
EXEC CreateUser
    @Email = 'user@example.com',
    @UserName = 'newuser',
    @Password = 'password123';

-- add user info sp
CREATE PROCEDURE AddUserInfo
    @Name NVARCHAR(255),
    @Age INT,
    @No_of_guests INT,
    @No_of_rooms INT,
    @Checkin DATETIME,
    @Checkout DATETIME
AS
BEGIN
    -- Insert user information into UserInfo table
    INSERT INTO UserInfo ([Name], Age, No_of_guests, No_of_rooms, Checkin, Checkout, Number_of_days)
    VALUES (@Name, @Age, @No_of_guests, @No_of_rooms, @Checkin, @Checkout, DATEDIFF(day, @Checkin, @Checkout));
    
    -- Retrieve the ID of the newly inserted record
    SELECT CAST(SCOPE_IDENTITY() AS INT) AS NewUserId;
END;

-- get user by uname and pwd
CREATE PROCEDURE GetUserByUsernameAndPassword
    @UserName NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM RegisteredUsers
    WHERE UserName = @UserName AND Password = @Password;
END;

-- get all rooms
CREATE PROCEDURE GetAllRooms
    @RoomType VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ri.RoomNumber, rt.Room_type
    FROM RoomInfo ri
    JOIN RoomType rt ON ri.RoomId = rt.Room_id
    WHERE rt.Room_type = @RoomType
        AND ri.isBooked = 0
        AND ri.Checkin = ri.Checkout;
END
