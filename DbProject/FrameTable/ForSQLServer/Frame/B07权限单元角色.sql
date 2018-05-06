IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JAuthorityRoles')
	BEGIN
	   DROP  Table JAuthorityRoles
	END
GO
  CREATE TABLE JAuthorityRoles
(
    num int,								--序号
    ID varchar(32) primary key,				--数据ID
    AuthorityID varchar(10) ,				--权限单元ID
	RoleID varchar(50)						--角色ID
)

/* drop table JAuthorityRoles */