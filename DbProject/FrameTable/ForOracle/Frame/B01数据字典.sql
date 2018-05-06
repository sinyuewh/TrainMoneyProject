/*-------数据字典--------*/
CREATE TABLE JItem
(
    num varchar(10),									
    ItemName varchar(50) primary key not null,			
    author varchar(50),								   
    createtime date ,								
    minvalue number,									
    maxvalue number,									
    intervalue number,									
    kind varchar(10),									
    modelID varchar(50),									
    remark varchar(200)								
)

