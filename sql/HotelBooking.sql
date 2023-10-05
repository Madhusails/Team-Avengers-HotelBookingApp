CREATE DATABASE HotelBookingDB

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
drop Table UserInfo

CREATE TABLE RegisteredUsers(
Id int PRIMARY KEY Identity(1,1),
Email VARCHAR(100),
UserName VARCHAR(50),
Password VARCHAR(50)
);


create table RoomType (
	RoomId int primary key,
	TypeOfRoom nvarchar(50) not null
	);

drop table RoomType
drop table RoomInfo

CREATE TABLE RoomInfo (
    RoomNumber int primary key identity(101, 1),
	RoomType varchar(25),
    Checkin DateTime2 default GETDATE(),
    Checkout DateTime2 default GETDATE(),
    IsBooked bit
    
);


drop table RoomInfo

insert into RoomType values(1, 'AC');
insert into RoomType values(2, 'Non-AC');
insert into RoomType values(3, 'AC Deluxe');
insert into RoomType values(4, 'Suit Room');

truncate table RoomInfo;
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);

insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);

insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('Non-AC',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);

insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);
insert into RoomInfo(RoomType,IsBooked) values('AC Deluxe',0);

insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);
insert into RoomInfo(RoomType,IsBooked) values('Suit Room',0);

select * from RoomInfo where RoomType='Non-AC' and isBooked=0

select ri.RoomNumber, rt.Room_type from RoomInfo ri 
join RoomType rt 
on ri.RoomId = rt.Room_id
where rt.Room_type='AC' and ri.isBooked=0 and ri.Checkin = ri.Checkout


select ri.RoomNumber,rt.TypeOfRoom from RoomInfo ri join RoomType rt on ri.RoomId=rt.RoomId



---stored procedures

CREATE PROCEDURE _uspAddUserInfo
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

CREATE PROCEDURE _uspGetUserByUsernameAndPassword
    @UserName NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

 

    SELECT *
    FROM RegisteredUsers
    WHERE UserName = @UserName AND Password = @Password;
END;


--stored procedure for registered users

CREATE PROCEDURE _uspAddRegisteredUser
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

---updateting the booking status of the room

CREATE PROCEDURE _uspUpdateRoomInfo
    @numberOfRooms INT,
    @RoomType VARCHAR(50),
    @Checkin DATE,
    @Checkout DATE
AS
BEGIN
    UPDATE TOP (@numberOfRooms) RoomInfo 
    SET IsBooked = 1, Checkin = @Checkin, Checkout = @Checkout 
    WHERE RoomType = @RoomType AND IsBooked = 0
END;


alter trigger UpdateIsBooked
on RoomInfo
after update
as
begin
	update RoomInfo
	set IsBooked = 0,
	Checkin= GETDATE(),
	Checkout=GetDate()
	where Checkout <= GETDATE()-1;
end;

select month('2023-05-10 00:00:00.0000000')

sp_helptext '_uspUpdateRoomInfo'

exec _uspUpdateRoomInfo
	@numberOfRooms = 3 , 
    @RoomType = 'AC',  
    @Checkin = '2023-10-04 00:00:00.0000000',  
    @Checkout = '2023-10-05 00:00:00.0000000'


CREATE PROCEDURE _uspDeleteUserByUsername
    @username NVARCHAR(50)
AS
BEGIN
    DELETE FROM RegisteredUsers 
    WHERE UserName = @username;
END;