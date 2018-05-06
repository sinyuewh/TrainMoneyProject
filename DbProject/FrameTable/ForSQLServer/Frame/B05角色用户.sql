IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JRoleUsers')
	BEGIN
	   DROP  Table JRoleUsers
	END
GO
 CREATE TABLE JRoleUsers
(
    num int,								--序号
    ID varchar(32) primary key,				--数据ID
    RoleID varchar(10) ,					--角色ID
	UserID varchar(50)						--用户ID
)

/* drop table JRoleUsers */