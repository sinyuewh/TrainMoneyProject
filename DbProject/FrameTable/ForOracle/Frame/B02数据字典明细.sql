 
/*-----数据字典明细-------*/
CREATE TABLE JItemDetail
(
    ID varchar(32) primary key,
    num number, 
    ItemName varchar(50) not null,
    itemText varchar(50) not null,
    itemValue varchar(50) not null
)
