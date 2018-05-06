  CREATE TABLE NewTrain
(							
	num int,										
	TrainType	varchar(50) not null,

	YinZuo int not null,          
    RuanZuo int not null,          
    OpenYinWo int not null,         
    CloseYinWo int not null,        
    RuanWo int not null,             
    AdvanceRuanWo int not null,  
    
    CanChe int not null,            
    FaDianChe int not null,         
    ShuYinChe int not null,          
    
    JiaKuai int,			
    KongTiaoFee int,
    
    QianYinType int,
    GongDianType int,
    
    ServerPerson int,
    YongCheDiShu number(18,2),
    CheDiShu number(18,2),
    
    HighTrainBianZhu int,
    HighTrainBigKind int,
    CunZengMoShi int  
)