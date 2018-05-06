CREATE TABLE GTTrainDragFee
(	
	num int not null primary key,									
	LineType varchar(150) not null,			
    CrossRoad varchar(150) not null,	
	MacType varchar(50) not null,
	DragFee int not null,
	NetFee  int
) 



create sequence GTTrainDragFee_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

/*----创建需要序列的表语句4----*/
create or replace trigger GTTrainDragFee_trigger 
before insert on GTTrainDragFee
for each row 
begin 
      select   GTTrainDragFee_Sequence.nextval   into:new.num from sys.dual ; 
end;