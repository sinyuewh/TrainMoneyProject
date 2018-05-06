/*-------1.模块分组--------*/
/*
 num:			序号
 groupname:		模块组名称
 groupICO:		模块组图标
 remark:		备注
 fname:			备用字段
*/
CREATE TABLE frm_modelgroup
(
    num number,									
    groupName varchar(50) primary key not null,											
    groupICO varchar(200),															
    remark varchar(200)							
)