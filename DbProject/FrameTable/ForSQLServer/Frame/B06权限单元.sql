IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'JAuthority')
	BEGIN
	   DROP  Table JAuthority
	END
GO
CREATE TABLE JAuthority
(
    num int,								--序号
    AuthorityID varchar(10) primary key,	--权限单元ID
	AuthorityName varchar(50) ,				--权限单元名称
	author varchar (50) ,					--作者
	createtime datetime  ,					--创建时间
	modelid varchar(50),					--模块ID
	remark varchar (200)					--备注
)

/* drop table JAuthority */