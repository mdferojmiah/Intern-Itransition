create database UserManagementDB;
use UserManagementDB;
create table Users(
	Id int auto_increment primary key,
    Name varchar(100) not null,
    Email varchar(100) not null,
    Password varchar(100) not null,
    RegistrationTime datetime not null,
    LastLoginTime datetime,
    Status enum('active', 'blocked') default 'active',
    IsDeleted bool default false
);
create unique index idx_users_email on Users(Email);

select * from users;