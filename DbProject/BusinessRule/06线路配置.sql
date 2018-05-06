  CREATE TABLE LineProfile
(
    id int ,									
	LineType	varchar(50)	not null primary key,
	LineName	varchar(50)	not null unique,
	Fee1	number(18,2),
	Fee2	number(18,2),
	Fee3	number(18,2),		
	Fee4	number(18,2),	
	Fee5	number(18,2),		
	Fee6	number(18,2)
)