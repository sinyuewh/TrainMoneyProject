-- 在此处添加您的测试方案 --
exec sp_configure 'show advanced options', '1';
go
reconfigure WITH OVERRIDE;  --加上override
go
exec sp_configure 'clr enabled', '1'
go
reconfigure WITH OVERRIDE;  -- 加上override
exec sp_configure 'show advanced options', '1';
go

