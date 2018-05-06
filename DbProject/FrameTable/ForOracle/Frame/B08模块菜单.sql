 /*-------3.模块菜单--------*/
/*
 num:						序号																				
 modelID：					模块ID	
 menuID：					菜单ID
 parentMenu：				父菜单ID
 menuName：					菜单名称		
 menuICO：					菜单图标
 menuUrl：					菜单URL,	
 menuTarget varchar(50),	菜单URL目标
 AuthorityID varchar(50),	菜单权限ID											
 remark varchar(200)		备注
*/
CREATE TABLE frm_modelmenu
(
    num number,																				
    modelID varchar(50) not null ,	
    menuID varchar(50) primary key not null,
    parentMenu varchar(50),
    menuName varchar(50),		
    menuICO varchar(200),
    menuUrl varchar(200),	
    menuTarget varchar(50),
    AuthorityID varchar(50),												
    remark varchar(200)								
)