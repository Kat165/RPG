AlTER PROC AddUser
@Name nvarchar(50),
@Password nvarchar(50),
@RoleId int
AS
	INSERT INTO Users(Name,Password,RoleId) VALUES (@Name,@Password,@RoleId)