/*-----创建序列-----*/
create sequence trainmoneydbSequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;


/*----创建需要序列的表语句1----*/
create or replace trigger LINESTATION_trigger 
before insert on LINESTATION
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.id from sys.dual ; 
end;



/*----创建需要序列的表语句2----*/
create or replace trigger NewTrainShouRou_trigger 
before insert on NewTrainShouRou
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.num from sys.dual ; 
end;

/*----创建需要序列的表语句3----*/
create or replace trigger NewTrainXianLuFee_trigger 
before insert on NewTrainXianLuFee
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.num from sys.dual ; 
end;


/*----创建需要序列的表语句3----*/
create or replace trigger NewTrainQianYinFee_trigger 
before insert on NewTrainQianYinFee
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.num from sys.dual ; 
end;


/*----创建需要序列的表语句4----*/
create or replace trigger NEWTRAINSERVERPEOPLE_trigger 
before insert on NEWTRAINSERVERPEOPLE
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.num from sys.dual ; 
end;


/*----创建需要序列的表语句4----*/
create or replace trigger bigStationList_trigger 
before insert on bigStationList
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.dataid from sys.dual ; 
end;


/*-----------------------------------------*/
create or replace trigger SEARCHOBJECTLIST_trigger 
before insert on SEARCHOBJECTLIST
for each row 
begin 
      select   trainmoneydbSequence.nextval   into:new.num from sys.dual ; 
end;

/*-----------------------------------------*/
create sequence TrainPerson_Sequence increment by 1 
start with 1 minvalue 1 maxvalue 9999999999999 nocache order;

create or replace trigger TrainPerson_trigger 
before insert on TrainPerson
for each row 
begin 
      select   TrainPerson_Sequence.nextval   into:new.num from sys.dual ; 
end;