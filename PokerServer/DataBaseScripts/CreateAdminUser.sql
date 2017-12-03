
-- Creates the login AbolrousHazem with password '340$Uuxwp7Mcxo7Khy'.  
CREATE LOGIN administrator  
    WITH PASSWORD = 'i@mDadm1n';  
GO  

-- Creates a database user for the login created above.  
CREATE USER administrator FOR LOGIN administrator
WITH DEFAULT_SCHEMA = dbo;  
GO  

GRANT CONNECT TO administrator;

GO
