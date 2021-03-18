RESTORE DATABASE [HangMan] FROM DISK = '/tmp/HangMan.bak'
WITH FILE = 1,
MOVE 'aspnet-HangMan-EEC0713E-5124-455A-B889-C77C46A9F065' To '/var/opt/mssql/data/HangMan.mdf',
MOVE 'aspnet-HangMan-EEC0713E-5124-455A-B889-C77C46A9F065_log' To '/var/opt/mssql/data/HangMan.ldf',
NOUNLOAD, REPLACE, STATS = 5
GO

