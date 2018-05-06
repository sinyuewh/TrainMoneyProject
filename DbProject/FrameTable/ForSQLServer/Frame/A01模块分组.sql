/*-------1.模块分组--------*/
/*
 num:			序号
 groupname:		模块组名称
 groupICO:		模块组图标
 remark:		备注
 fname:			备用字段
*/

IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'frm_modelgroup')
	BEGIN
	   DROP  Table frm_modelgroup
	END
GO
CREATE TABLE frm_modelgroup
(
    num int,									
    groupName varchar(50) primary key not null,											
    groupICO varchar(200),															
    remark varchar(200)							
)