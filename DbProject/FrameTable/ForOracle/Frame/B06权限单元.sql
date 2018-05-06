CREATE TABLE JAuthority
(
    num number,								--序号
    AuthorityID varchar(10) primary key,	--权限单元ID
	AuthorityName varchar(50) ,				--权限单元名称
	author varchar (50) ,					--作者
	createtime date  ,						--名称
	modelid varchar(50),					--模块ID
	remark varchar (200)					--备注
)

/* drop table JAuthority */