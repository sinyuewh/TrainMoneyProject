/*-------2.系统模块--------*/
/*
 num:			序号
 modelID:		模块ID
 modelName：	模块名称
 groupName:		模块组名称
 modelICO:		模块图标
 modelURL:		模块URL
 remark:		备注
*/
CREATE TABLE frm_model
(
    num number,									
    modelID varchar(50) primary key not null,											
    modelName varchar(50) not null unique,	
    groupName varchar(50),		
    modelICO varchar(200),
    modelUrl varchar(200),													
    remark varchar(200)								
)
