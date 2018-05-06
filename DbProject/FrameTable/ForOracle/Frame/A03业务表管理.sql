
/*-------4.业务表管理--------*/
/*
num：			序号									
modelID：		模块ID											
tableID：		表ID,	
tableCaption：	表描述, 
className：		业务类名称,
Responsible：	责任人,														
remark：		备注	
*/
CREATE TABLE frm_systable
(
    num number,									
    modelID varchar(50) ,											
    tableID varchar(50) not null primary key,	
    tableCaption varchar(50), 
    className varchar(50),
    Responsible varchar(50),														
    remark varchar(200)								
)