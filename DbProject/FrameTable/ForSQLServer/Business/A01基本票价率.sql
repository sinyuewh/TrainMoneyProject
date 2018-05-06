/*-------1.基本票价率--------*/
IF EXISTS (SELECT * FROM sysobjects WHERE type = 'U' AND name = 'u_BaseFare')
	BEGIN
	   DROP  Table u_BaseFare
	END
GO
CREATE TABLE u_BaseFare
(
    ID int primary key,									
    bigkind nvarchar(50) not null,	
    smallkind nvarchar(50),										
    FareRate numeric(18,2),
    remark nvarchar(200)				
)