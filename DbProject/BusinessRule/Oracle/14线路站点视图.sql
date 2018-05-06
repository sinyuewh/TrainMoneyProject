create or replace view linestationview as
select LINESTATION."ID",
bigstationlist.name1 aname1,
b2.name1 bname1,
LINESTATION."LINEID",LINESTATION."NUM",LINESTATION."DQH",LINESTATION."ASTATION",LINESTATION."BSTATION",LINESTATION."MILES",
LINESTATION."MILESCLASS",LINESTATION."DIRECTION",LINESTATION."JNFLAG",TRAINLINE.LineType,TRAINLINE.LineName
   from LINESTATION inner join TRAINLINE on LineStation.LineID=TRAINLINE.LineID
   left outer join bigstationlist on linestation.astation=bigstationlist.name1
   left outer join bigstationlist b2 on linestation.bstation=b2.name1