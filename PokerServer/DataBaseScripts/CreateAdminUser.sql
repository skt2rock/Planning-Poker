
-- Creates the login AbolrousHazem with password '340$Uuxwp7Mcxo7Khy'.  
CREATE LOGIN [admin]  
    WITH PASSWORD = 'i@mDadm1n';  
GO  

-- Creates a database user for the login created above.  
CREATE USER [admin] FOR LOGIN [admin]
WITH DEFAULT_SCHEMA = dbo;  
GO  

GRANT CONNECT TO [admin];

GO
