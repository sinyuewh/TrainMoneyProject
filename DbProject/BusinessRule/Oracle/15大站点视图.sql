create or replace view bigStationview as
select BIGSTATIONLIST.*,Fee1,fee2,Fee3 from  BIGSTATIONLIST inner join BIGSTATION
on BIGSTATIONLIST.PARENTNUM=BIGSTATION.num