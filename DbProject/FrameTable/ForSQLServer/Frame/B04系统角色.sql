﻿IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JRole')
	BEGIN
	   DROP  Table JRole
	END
GO
CREATE TABLE JRole
(
    num int,								--序号
    RoleID varchar(10) primary key,			--角色ID
	RoleName varchar(50) ,					--角色名称
	modelid varchar(50),					--模块ID
	departflag varchar(10),					--部门管理
	remark varchar (200)					--备注
)

/* drop table JRole */